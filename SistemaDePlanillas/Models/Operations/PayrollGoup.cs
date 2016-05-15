using DevOne.Security.Cryptography.BCrypt;
using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net.Mail;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {

        public static void calculate_setAsReady(User user)
        {
            var location = DBManager.Instance.locations.getLocation(user.Location);
            if (location.CurrentPayroll != null)
            {
                DBManager.Instance.locations.setPendingToApprove(user.Location, true);
                Mail("fgomeza25@gmail.com", "Aprobación de Planillas Coopesuperación",
                String.Format("Hola! \n El usuario {0} del sistema de planillas de Coopesuperación ha realizado el cálculo de planillas y este esta pendiente de aprobación.\n", user.Name));
            }
            else
            {
                IErrors.validateException(App_LocalResoures.Errors.invalidProcedure);
            }

        }

        public static void calculate_cancel(User user)
        {
            var location = DBManager.Instance.locations.getLocation(user.Location);
            if (!location.isPendingToApprove)
                DBManager.Instance.locations.updateLocationCurrentPayroll(user.Location, null);
            else
                IErrors.validateException(App_LocalResoures.Errors.invalidProcedure);
        }

        private static void Mail(string to, string subject, string message)
        {
            MailMessage mail = new MailMessage(new MailAddress("coopesuperacion@gmx.es", "Sistema de Planillas Coopesuperación"), new MailAddress(to));
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("coopesuperacion@gmx.es", "coopesuperacion");
            client.Host = "smtp.gmx.es";
            mail.Subject = subject;
            mail.Body = message;
            client.Send(mail);
        }

        public static void reprove(User user)
        {
            var location = DBManager.Instance.locations.getLocation(user.Location);
            if(location.CurrentPayroll==null || !location.isPendingToApprove)
            {
                IErrors.validateException(App_LocalResoures.Errors.invalidProcedure);
            }
            DBManager.Instance.locations.updateLocationCurrentPayroll(user.Location,null);
            DBManager.Instance.locations.setPendingToApprove(user.Location,false);
        }

        public static void aprove(User user)
        {
            var location = DBManager.Instance.locations.getLocation(user.Location);
            if (location.CurrentPayroll == null || !location.isPendingToApprove)
            {
                IErrors.validateException(App_LocalResoures.Errors.invalidProcedure);
            }
            var current = DBManager.Instance.payrolls.selectPayroll((long)location.CurrentPayroll);
            var employees = DBManager.Instance.employees.selectAllActiveEmployees(user.Location);
            double callPrice = location.CallPrice;
            foreach (var employee in employees)
            {
                var vacation = DBManager.Instance.employees.selectVacations(employee.id, current.endDate);
                double totalVacation = vacation.Sum(v => v.vacationPrice);
                long workedDays = (current.endDate - (employee.activeSince > current.initialDate ? employee.activeSince : current.initialDate)).Days - vacation.Count;
                var calls = DBManager.Instance.employees.callListByEmployee(employee.id, current.endDate);
                long callsCount = calls.Sum(c => c.calls);
                var penalties = DBManager.Instance.penalties.selectAllPenalty(employee.id, current.endDate);
                double totalPenalties = penalties.Sum(p => p.amount * p.penaltyPrice);
                var fixedDebits = DBManager.Instance.debits.selectFixedDebits(employee.id);
                double totalFixedDebits = fixedDebits.Sum(d => d.amount);
                var paymentDebits = DBManager.Instance.debits.selectPaymentDebits(employee.id);
                double totalPaymentDebits = paymentDebits.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                var amortizationDebits = DBManager.Instance.debits.selectAmortizationDebits(employee.id);
                double totalAmortizationDebits = amortizationDebits.Sum(d => calculateAmortization(d.total, d.missingPayments + d.paymentsMade, d.interestRate));
                double grossAmount = (employee.salary / location.workingDaysPerMonth) * workedDays + (callsCount * callPrice);
                var lastSalaries = DBManager.Instance.salaries.getLastSalaries(employee.id);
                double extraPrice = (grossAmount + lastSalaries.Sum(s => s.salary) / lastSalaries.Sum(s => s.workedDays) + workedDays) / location.workingHoursPerDay;
                var extras = DBManager.Instance.extras.selectExtras(employee.id, current.endDate);
                long extraCount = extras.Sum(e => e.hours);
                double totalExtras = extraCount * extraPrice;
                double saving = DBManager.Instance.salaries.selectSavingByEmployee(employee.id);
                double netSalary = grossAmount + totalExtras + totalVacation - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits - totalAmortizationDebits - totalFixedDebits - employee.negativeAmount;

                DBManager.Instance.salaries.assignSalaryToPayroll(employee.id, current.id, grossAmount, netSalary);

                paymentDebits.ForEach(p =>DBManager.Instance.debits.payDebit(p.id, current.id));

                amortizationDebits.ForEach(p =>DBManager.Instance.debits.payDebit(p.id, current.id));
            }
            DBManager.Instance.calls.assignCallsToPayroll(current.id, location.Id, current.endDate);
            DBManager.Instance.extras.assignExtrasToPayroll(current.id, location.Id, current.endDate);
            DBManager.Instance.penalties.assignPenaltiesToPayroll(current.id, location.Id, current.endDate);
            DBManager.Instance.employees.assingVacationsToPayroll(current.id, location.Id, current.endDate);
            DBManager.Instance.locations.updateLocationLastPayroll(location.Id);
        }

        public static object calculate(User user, DateTime initialDate, DateTime endDate)
        {
            var location = DBManager.Instance.locations.getLocation(user.Location);
            if (location.LastPayroll != null && DBManager.Instance.payrolls.selectPayroll((long)location.LastPayroll).endDate > initialDate)
                IErrors.validateException(App_LocalResoures.Errors.invalidInitialDate);
            if (endDate > initialDate || endDate > DateTime.Today)
                IErrors.validateException(App_LocalResoures.Errors.invalidDate);
            
            if (location.isPendingToApprove || location.CurrentPayroll != null)
            {
                IErrors.validateException(App_LocalResoures.Errors.invalidProcedure);
            }

            var employees = DBManager.Instance.employees.selectAllActiveEmployees(user.Location);
            double totalPayroll = 0;
            double callPrice = location.CallPrice;
            List<object> rows = new List<object>();
            foreach (var employee in employees)
            {
                var vacation = DBManager.Instance.employees.selectVacations(employee.id, endDate);
                double totalVacation = vacation.Sum(v=>v.vacationPrice);
                long workedDays = (endDate - (employee.activeSince > initialDate ?employee.activeSince:initialDate)).Days - vacation.Count;
                var calls = DBManager.Instance.employees.callListByEmployee(employee.id, endDate);
                long callsCount = calls.Sum(c => c.calls);
                var penaltiesDB = DBManager.Instance.penalties.selectAllPenalty(employee.id, endDate);
                var penalties = formatPenalties(penaltiesDB);
                double totalPenalties = penaltiesDB.Sum(p => p.amount * p.penaltyPrice);
                var fixedDebitsDB = DBManager.Instance.debits.selectFixedDebits(employee.id);
                var fixedDebits = formatFixedDebits(fixedDebitsDB,workedDays);
                double totalFixedDebits = fixedDebitsDB.Sum(d => (d.amount/d.period)*workedDays);
                var paymentDebitsDB = DBManager.Instance.debits.selectPaymentDebits(employee.id);
                var paymentDebits = formatPaymentDebits(paymentDebitsDB,workedDays);
                double totalPaymentDebits = paymentDebitsDB.Sum(d => (d.remainingAmount / d.missingPayments) + d.total * d.interestRate);
                var amortizationDebitsDB = DBManager.Instance.debits.selectAmortizationDebits(employee.id);
                var amortizationDebits = formatAmortizationDebits(amortizationDebitsDB,workedDays);
                double totalAmortizationDebits = amortizationDebitsDB.Sum(d => calculateAmortization(d.total, d.missingPayments + d.paymentsMade, d.interestRate));
                double grossAmount = (employee.salary / location.workingDaysPerMonth)*workedDays + (callsCount * callPrice);
                var lastSalaries = DBManager.Instance.salaries.getLastSalaries(employee.id);
                double extraPrice = (grossAmount + lastSalaries.Sum(s=>s.salary)/lastSalaries.Sum(s=>s.workedDays)+workedDays) / location.workingHoursPerDay;
                var extras = DBManager.Instance.extras.selectExtras(employee.id,endDate);
                long extraCount = extras.Sum(e => e.hours);
                double totalExtras = extraCount * extraPrice;
                double saving = DBManager.Instance.salaries.selectSavingByEmployee(employee.id);
                double netSalary = grossAmount + totalExtras + totalVacation - grossAmount * location.Capitalization - totalPenalties - totalPaymentDebits - totalAmortizationDebits - totalFixedDebits - employee.negativeAmount;

                totalPayroll += netSalary > 0 ? netSalary : 0;
                rows.Add(new
                {
                    employee = employee.name,
                    calls = new { count = callsCount, total = callsCount * callPrice, list = calls },
                    penalties = new { count = penaltiesDB.Sum(p => p.amount), total = totalPenalties, list = penalties },
                    extras = new { count = extraCount, total = totalExtras, list = extras },
                    fixedDebits = fixedDebits,
                    paymentDebits = paymentDebits,
                    amortizationDebits = amortizationDebits,
                    salary = grossAmount,
                    saving = new { monthlyAmount = grossAmount * location.Capitalization, total = saving + grossAmount * location.Capitalization },
                    netSalary = netSalary,
                    vacations= new { count = vacation.Count, total=totalVacation, list=vacation},
                    negativeAmount = netSalary > 0 ? 0 : netSalary
                });
            }
            var javaScriptSerializer = new JavaScriptSerializer();
            var payroll = new { initialDate = initialDate, endDate = endDate, totalPayroll = totalPayroll, employees = rows };
            var current = DBManager.Instance.payrolls.addPayroll(initialDate,endDate, callPrice, user.Id, javaScriptSerializer.Serialize(payroll), user.Location);
            DBManager.Instance.locations.updateLocationCurrentPayroll(user.Location, current.id);
            return payroll;
        }


        private static List<object> formatPaymentDebits(List<PaymentDebit> debitsByEmployee, long workedDays)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(d =>
                ((d.remainingAmount / d.missingPayments) + d.total * d.interestRate) * payments(d, workedDays)),
                list = list.Where(d => payments(d, workedDays) > 0).Select(d => new
                {
                    debitId = d.id,
                    description = d.detail,
                    payments = payments(d, workedDays),
                    total = ((d.remainingAmount / d.missingPayments) + d.total * d.interestRate) *
                      payments(d, workedDays)
                })
                }).ToList();
        }

        private static long payments(PaymentDebit d, long workedDays)
        {
            return Math.Min(((workedDays + d.pastDays) / d.period), d.missingPayments);
        }
        private static long payments(AmortizationDebit d, long workedDays)
        {
            return Math.Min(((workedDays + d.pastDays) / d.period), d.missingPayments);
        }
        private static long payments(Debit d, long workedDays)
        {
            return (workedDays + d.pastDays) / d.period;
        }

        private static List<object> formatAmortizationDebits(List<AmortizationDebit> debitsByEmployee, long workedDays)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(d => calculateAmortization(d.total, d.missingPayments + d.paymentsMade, d.interestRate) * payments(d,workedDays)),
                list = list.Where(d => payments(d, workedDays) > 0).Select(d => new
                {
                    debitId = d.id,
                    description = d.detail,
                    payments = payments(d, workedDays),
                    total = calculateAmortization(d.total, d.missingPayments + d.paymentsMade, d.interestRate) * payments(d, workedDays)
                })
            }).ToList();
        }

        private static List<object> formatFixedDebits(List<Debit> debitsByEmployee,long workedDays)
        {
            return debitsByEmployee.GroupBy(debit => debit.typeName, debit => debit, (name, list) => (object)new
            {
                typeName = name,
                count = list.Count(),
                total = list.Sum(d => d.amount * payments(d, workedDays)),
                list = list.Where(d=>payments(d, workedDays) > 0).Select(d=>new
                {
                    debitId =d.id,
                    description=d.detail,
                    payments = ((workedDays + d.pastDays) / d.period),
                    total = d.amount * payments(d,workedDays)
                })
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

        public static double calculateAmortization(double total, long pays, double interestRate)
        {

            //A = 1-(1+taza)^-plazos
            long p = pays * -1;
            double b = (1 + interestRate);
            double A = (1 - Math.Pow(b, p)) / interestRate;

            //Cuota Fija = Monto / A;
            return total / A;
        }
    }

}


