using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {


        public static Response calculate(User user, DateTime initialDate, DateTime endDate)
        {
            var employees = DBManager.Instance.selectAllActiveEmployees(user.Location).Detail;
            double totalPayroll = 0;
            var rows = employees.Select(employee =>
              {
                  var calls = DBManager.Instance.callListByEmployee(employee.id, endDate).Detail;
                  long totalCalls = calls.Sum(c => c.calls);
                  var location = DBManager.Instance.getLocation(user.Location).Detail;
                  double callPrice = location.CallPrice;
                  var penaltiesDB = DBManager.Instance.selectAllPenalty(employee.id, endDate).Detail;
                  var penalties = penaltiesByEmployee(penaltiesDB);
                  double totalPenalties = penaltiesDB.Sum(p => p.amount * p.penaltyPrice);
                  var extras = DBManager.Instance.selectExtras(employee.id).Detail;
                  double totalExtras = extras.Sum(e => e.amount);
                  var fixedDebitsDB = DBManager.Instance.selectDebits(employee.id).Detail;
                  var fixedDebits = fixedDebitsByEmployee(fixedDebitsDB);
                  double totalFixedDebits = fixedDebitsDB.Sum(d => d.amount);
                  var paymentDebitsDB = DBManager.Instance.selectPaymentDebits(employee.id).Detail;
                  var paymentDebits = paymentDebitsByEmployee(paymentDebitsDB);
                  double totalPaymentDebits = paymentDebitsDB.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                  double grossAmount = (employee.salary / 30.4167) * (endDate - initialDate).Days + (totalCalls * callPrice);
                  double saving = DBManager.Instance.selectSavingByEmployee(employee.id).Detail;
                  double netSalary = grossAmount + totalExtras - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits - totalFixedDebits - employee.negativeAmount;

                  totalPayroll += netSalary > 0 ? netSalary : 0;
                  return new
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
                  };
              });
            return Responses.WithData(rows);
        }

        private static List<object> paymentDebitsByEmployee(List<PaymentDebit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                TypeName = name,
                amount = list.Count(),
                total = list.Sum(debit => (debit.remainingAmount / debit.missingPayments) + debit.total * debit.interestRate),
                debitList = list
            }).ToList();
        }

        private static List<object> fixedDebitsByEmployee(List<Debit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                TypeName = name,
                amount = list.Count(),
                total = list.Sum(debit => debit.amount),
                debitList = list
            }).ToList();
        }

        private static List<object> penaltiesByEmployee(List<Penalty> penaltiesByEmployee)
        {
            return penaltiesByEmployee.GroupBy(penalty => penalty.typeName, penalty => penalty, (name, lista) => (object)new
            {
                typeName = name,
                amount = lista.Sum(penalty => penalty.amount),
                total = lista.Sum(penalty => penalty.amount * penalty.penaltyPrice),
                typeList = lista
            }).ToList();
        }
    }

}