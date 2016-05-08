﻿using DevOne.Security.Cryptography.BCrypt;
using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {
        public static long workHoursByMonth = 208;

        public static object calculate(User user, DateTime initialDate, DateTime endDate)
        {
            long days = (endDate - initialDate).Days;
            var employees = DBManager.Instance.employees.selectAllActiveEmployees(user.Location);
            double totalPayroll = 0;
            var location = DBManager.Instance.locations.getLocation(user.Location);
            double callPrice = location.CallPrice;
            List<object> rows = new List<object>();
            foreach (var employee in employees)
            {
                var calls = DBManager.Instance.employees.callListByEmployee(employee.id, endDate);
                long callsCount = calls.Sum(c => c.calls);
                var penaltiesDB = DBManager.Instance.penalties.selectAllPenalty(employee.id, endDate);
                var penalties = formatPenalties(penaltiesDB);
                double totalPenalties = penaltiesDB.Sum(p => p.amount * p.penaltyPrice);
                var fixedDebitsDB = DBManager.Instance.debits.selectDebits(employee.id);
                var fixedDebits = formatFixedDebits(fixedDebitsDB,days);
                double totalFixedDebits = fixedDebitsDB.Sum(d => d.amount);
                var paymentDebitsDB = DBManager.Instance.debits.selectPaymentDebits(employee.id);
                var paymentDebits = formatPaymentDebits(paymentDebitsDB);
                double totalPaymentDebits = paymentDebitsDB.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                var amortizationDebitsDB = DBManager.Instance.debits.selectAmortizationDebits(employee.id);
                var amortizationDebits = formatAmortizationDebits(amortizationDebitsDB);
                double totalAmortizationDebits = amortizationDebitsDB.Sum(d => calculateAmortization(d.total, d.missingPayments + d.paymentsMade, d.interestRate));
                double grossAmount = (employee.salary / 2) + (callsCount * callPrice);
                var lastSalaries = DBManager.Instance.salaries.getLastSalaries(employee.id);
                double extraPrice = (grossAmount + lastSalaries.Sum() / (lastSalaries.Count + 1))/ workHoursByMonth;
                var extras = DBManager.Instance.extras.selectExtras(employee.id);
                long extraCount = extras.Sum(e => e.hours);
                double totalExtras = extras.Sum(e => e.hours)*extraPrice;
                double saving = DBManager.Instance.debits.selectSavingByEmployee(employee.id);
                double netSalary = grossAmount + totalExtras - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits -totalAmortizationDebits - totalFixedDebits - employee.negativeAmount;

                totalPayroll += netSalary > 0 ? netSalary : 0;
                rows.Add(new
                {
                    employee = employee.name,
                    calls = new { count = callsCount, total = callsCount * callPrice, list = calls },
                    penalties = new { count = penaltiesDB.Sum(p => p.amount), total = totalPenalties, list = penalties },
                    extras = new { count=extraCount ,total = totalExtras, list = extras },
                    fixedDebits = fixedDebits,
                    paymentDebits = paymentDebits,
                    amortizationDebits = amortizationDebits,
                    salary = grossAmount,
                    saving = new { monthlyAmount = grossAmount * location.Capitalization, total = saving + grossAmount * location.Capitalization },
                    netSalary = netSalary,
                    negativeAmount = netSalary>0?0:netSalary
                });
            }
           // var javaScriptSerializer = new JavaScriptSerializer();
            var payroll = new { initialDate = initialDate, endDate = endDate, totalPayroll = totalPayroll, employees = rows };
           // var current =DBManager.Instance.addPayroll(endDate,callPrice, user.Id, javaScriptSerializer.Serialize(payroll), user.Location);
          //DBManager.Instance.updateLocationCurrentPayroll(user.Location, current.id);

            return Responses.WithData(payroll);
        }

        private static List<object> formatPaymentDebits(List<PaymentDebit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(debit => (debit.remainingAmount / debit.missingPayments) + debit.total * debit.interestRate),
                list = list
            }).ToList();
        }

        private static List<object> formatAmortizationDebits(List<AmortizationDebit> debitsByEmployee)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(d=>calculateAmortization(d.total,d.missingPayments+d.paymentsMade,d.interestRate)),
                list = list
            }).ToList();
        }

        private static List<object> formatFixedDebits(List<Debit> debitsByEmployee, long days)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(debit => debit.amount),
                list = list
            }).ToList();

        }

        private static List<object> formatPenalties(List<Penalty> penaltiesByEmployee)
        {
            return penaltiesByEmployee.GroupBy(penalty => penalty.typeName, penalty => penalty, (name, lista) => (object)new
            {
                typeName = name,
                count = lista.Sum(penalty => penalty.amount),
                total = lista.Sum(penalty => penalty.amount * penalty.penaltyPrice),
                list = lista
            }).ToList();
        }

        private static double calculateAmortization(double total, long months, double interestRate)
        {

            //A = 1-(1+taza)^-plazos
            long p = months * -1;
            double b = (1 + interestRate);
            double A = (1 - Math.Pow(b, p)) / interestRate;

            //Cuota Fija = Monto / A;
            return total / A;
        }
    }

}


