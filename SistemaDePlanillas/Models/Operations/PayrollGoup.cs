using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {


        public static Response calculate(User user,DateTime initialDate, DateTime endDate)
        {
            List<object> rows= new List<object>();
            var employees = DBManager.Instance.selectAllActiveEmployees(user.Location).Detail;
            double totalPayroll = 0;
            foreach (var employee in employees)
            {
                var calls = DBManager.Instance.callListByEmployee(employee.id,endDate).Detail;
                long totalCalls = calls.Sum(c => c.calls);
                var location = DBManager.Instance.getLocation(user.Location).Detail;
                double callPrice = location.CallPrice;
                var penaltiesDB = DBManager.Instance.selectAllPenalty(employee.id, endDate).Detail;
                var penalties = penaltiesByEmployee(penaltiesDB);
                double totalPenalties = penaltiesDB.Sum(p=>p.amount* p.penaltyPrice);
                var extras = DBManager.Instance.selectExtras(employee.id).Detail;
                double totalExtras = extras.Sum(e => e.amount);
                var fixedDebitsDB = DBManager.Instance.selectDebits(employee.id).Detail;
                var fixedDebits = fixedDebitsByEmployee(fixedDebitsDB);
                double totalFixedDebits = fixedDebitsDB.Sum(d=>d.amount);
                var paymentDebitsDB = DBManager.Instance.selectPaymentDebits(employee.id).Detail;
                var paymentDebits = paymentDebitsByEmployee(paymentDebitsDB);
                double totalPaymentDebits = paymentDebitsDB.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                double grossAmount = (employee.salary/ 30.4167)*(endDate-initialDate).Days + (totalCalls * callPrice);
                double saving = DBManager.Instance.selectSavingByEmployee(employee.id).Detail;
                double netSalary = grossAmount + totalExtras - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits - totalFixedDebits - employee.negativeAmount;
                rows.Add(new
                {
                    employee = employee.name,
                    calls = new { amount = totalCalls, total = totalCalls * callPrice, callList = calls },
                    penalties = penalties,
                    extras = new { total = totalExtras, extraList = extras },
                    fixedDebits = fixedDebits,
                    paymentDebits = paymentDebits,
                    salary = grossAmount,
                    saving = new { amount = grossAmount * location.Capitalization, total = saving + grossAmount * location.Capitalization },
                    netSalary = netSalary,
                    negativeAmount = employee.negativeAmount
                });
                totalPayroll += netSalary > 0 ? netSalary : 0;
            }

            return Responses.WithData(new { initialDate = initialDate, endDate = endDate, employees = rows, total = totalPayroll });

        }


        private static List<object> paymentDebitsByEmployee(List<PaymentDebit> debitsByEmployee)
        {
            Dictionary<string, List<PaymentDebit>> debitsByType = new Dictionary<string, List<PaymentDebit>>();
            foreach (var debit in debitsByEmployee)
            {
                if (debitsByType.ContainsKey(debit.typeName))
                    debitsByType[debit.typeName].Add(debit);
                else
                    debitsByType.Add(debit.typeName, new List<PaymentDebit>() { debit });
            }
            List<object> debits = new List<object>();
            foreach (var debit in debitsByType)
            {
                debits.Add(new
                {
                    TypeName = debit.Key,
                    amount = debit.Value.Count,
                    total = debit.Value.Sum(d => (d.remainingAmount/d.missingPayments)+d.total*d.interestRate), 
                    debitList = debit.Value
                });
            }
            return debits;
        }

        private static List<object> fixedDebitsByEmployee(List<Debit> debitsByEmployee)
        {
            Dictionary<string, List<Debit>> debitsByType = new Dictionary<string, List<Debit>>();
            foreach (var debit in debitsByEmployee)
            {
                if (debitsByType.ContainsKey(debit.typeName))
                     debitsByType[debit.typeName].Add(debit);
                else
                    debitsByType.Add(debit.typeName, new List<Debit>() { debit});
            }
            List<object> debits = new List<object>();
            foreach(var debit in debitsByType)
            {
                debits.Add(new { TypeName = debit.Key, amount =debit.Value.Count, total = debit.Value.Sum(d=>d.amount), debitList = debit.Value});
            }
            return debits;
        }

        private static  List<object> penaltiesByEmployee(List<Penalty> penaltiesByEmployee)
        {
            
            Dictionary<string, List<Penalty>> penaltiesByType = new Dictionary<string, List<Penalty>>();
            foreach (var penalty in penaltiesByEmployee)
            {
                if (penaltiesByType.ContainsKey(penalty.typeName))
                    penaltiesByType[penalty.typeName].Add(penalty);
                else
                    penaltiesByType.Add(penalty.typeName, new List<Penalty>() { penalty });
            }
            List<object> penalties = new List<object>();
            foreach (var penalty in penaltiesByType)
            {
                penalties.Add(new { typeName = penalty.Key, amount = penalty.Value.Sum(p => p.amount), total = penalty.Value.Sum(p => p.amount * p.penaltyPrice), typeList = penalty.Value });
            }
            return penalties;
        }
    }

}