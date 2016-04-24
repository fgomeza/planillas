using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {
        public static long workHoursByMonth = 208;

        public static Response password(User user, string password)
        {
            return Responses.WithData(BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()));
        }

        public static Response xxx(User user, double total, long mo, double inte)
        {

                //formula para calcular cuota fija
                ///de un prestamo usando el metodo frances de cuota fija

                double Monto = total;
                long Plazos = mo;
                double taza = inte;


                //A = 1-(1+taza)^-plazos
                long p = Plazos * -1;
                double b = (1 + taza);
                double A = (1 - Math.Pow(b, p)) / taza;

                //Cuota Fija = Monto / A;
                double Cuota_F = Monto / A;


            return Responses.WithData(Cuota_F);
    
}

        public static Response calculate(User user, DateTime initialDate, DateTime endDate)
        {
            var employees = DBManager.Instance.selectAllActiveEmployees(user.Location).Detail;
            double totalPayroll = 0;
            var location = DBManager.Instance.getLocation(user.Location).Detail;
            double callPrice = location.CallPrice;
            List<object> rows = new List<object>();
            foreach (var employee in employees)
            {
                var calls = DBManager.Instance.callListByEmployee(employee.id, endDate).Detail;
                long totalCalls = calls.Sum(c => c.calls);
                var penaltiesDB = DBManager.Instance.selectAllPenalty(employee.id, endDate).Detail;
                var penalties = penaltiesByEmployee(penaltiesDB);
                double totalPenalties = penaltiesDB.Sum(p => p.amount * p.penaltyPrice);
                var fixedDebitsDB = DBManager.Instance.selectDebits(employee.id).Detail;
                var fixedDebits = fixedDebitsByEmployee(fixedDebitsDB);
                double totalFixedDebits = fixedDebitsDB.Sum(d => d.amount);
                var paymentDebitsDB = DBManager.Instance.selectPaymentDebits(employee.id).Detail;
                var paymentDebits = paymentDebitsByEmployee(paymentDebitsDB);
                double totalPaymentDebits = paymentDebitsDB.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                double grossAmount = (employee.salary / 2) + (totalCalls * callPrice);
                var lastSalaries = DBManager.Instance.getLastSalaries(employee.id).Detail;
                var extraPrice = (grossAmount + lastSalaries.Sum() / lastSalaries.Count + 1)/ workHoursByMonth;
                var extras = DBManager.Instance.selectExtras(employee.id).Detail;
                double totalExtras = extras.Sum(e => e.hours)*extraPrice;
                double saving = DBManager.Instance.selectSavingByEmployee(employee.id).Detail;
                double netSalary = grossAmount + totalExtras - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits - totalFixedDebits - employee.negativeAmount;

                totalPayroll += netSalary > 0 ? netSalary : 0;
                rows.Add(new
                {
                    employee = employee.name,
                    calls = new { count = totalCalls, total = totalCalls * callPrice, callList = calls },
                    penalties = new { count = penaltiesDB.Sum(p => p.amount), total = totalPenalties, penaltyList = penalties },
                    extras = new { total = totalExtras, extraList = extras },
                    fixedDebits = fixedDebits,
                    paymentDebits = paymentDebits,
                    salary = grossAmount,
                    saving = new { monthlyAmount = grossAmount * location.Capitalization, total = saving + grossAmount * location.Capitalization },
                    netSalary = netSalary,
                    negativeAmount = employee.negativeAmount
                });
            }
            var payroll = new { initialDate = initialDate, endDate = endDate, totalPayroll = totalPayroll, employees = rows };
            return Responses.WithData(payroll);
        }

        private static List<object> paymentDebitsByEmployee(List<PaymentDebit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                TypeName = name,
                count = list.Count(),
                total = list.Sum(debit => (debit.remainingAmount / debit.missingPayments) + debit.total * debit.interestRate),
                debitList = list
            }).ToList();
        }

        private static List<object> fixedDebitsByEmployee(List<Debit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                TypeName = name,
                count = list.Count(),
                total = list.Sum(debit => debit.amount),
                debitList = list
            }).ToList();
        }

        private static List<object> penaltiesByEmployee(List<Penalty> penaltiesByEmployee)
        {
            return penaltiesByEmployee.GroupBy(penalty => penalty.typeName, penalty => penalty, (name, lista) => (object)new
            {
                typeName = name,
                count = lista.Sum(penalty => penalty.amount),
                total = lista.Sum(penalty => penalty.amount * penalty.penaltyPrice),
                typeList = lista
            }).ToList();
        }
    }

}


