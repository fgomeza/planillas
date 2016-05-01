using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static Debit add_Fixed(User user, long employee, string detail, double amount, long type)
        {
            return DBManager.Instance.addFixedDebit(employee, detail, amount, type);
        }

        public static PaymentDebit add_Payment(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long pays, long type)
        {

            return DBManager.Instance.addPaymentDebit(employee, initialDate, detail, total, pays, type);
        }
        public static AmortizationDebit add_Amortization(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long pays, long type)
        {

            return DBManager.Instance.addAmortizationDebit(employee, initialDate, detail, total, pays, type);
        }

        public static List<Debit> get_AllFixed(User user, long employee)
        {
            return DBManager.Instance.selectDebits(employee);
        }

        public static List<PaymentDebit> get_AllPayment(User user, long employee)
        {
            return DBManager.Instance.selectPaymentDebits(employee);
        }

        public static List<AmortizationDebit> get_AllAmortization(User user, long employee)
        {
            return DBManager.Instance.selectAmortizationDebits(employee);
        }

        public static Debit get_Fixed(User user, long idDebit)
        {
            return DBManager.Instance.selectFixedDebit(idDebit);
        }

        public static PaymentDebit get_Payment(User user, long idDebit)
        {
            return DBManager.Instance.selectPaymentDebit(idDebit);  
        }

        public static AmortizationDebit get_Amortization(User user, long idDebit)
        {
            return DBManager.Instance.selectAmortizationDebit(idDebit);
        }

        public static void modify_Fixed(User user, long idDebit, string detail, double amount)
        {
            DBManager.Instance.updateFixedDebit(idDebit, detail, amount);
        }

        public static void modify_Payment(User user, long idDebit, double total, long remainingMonths)
        {
            DBManager.Instance.updatePaymentDebit(idDebit, total, remainingMonths);
        }

        public static void remove_Fixed(User user, long idDebit)
        {
            DBManager.Instance.deleteDebit(idDebit);
        }

        public static void remove_Payment(User user, long idDebit)
        {
            DBManager.Instance.deleteDebit(idDebit);
        }

        public static void remove_Amortization(User user, long idDebit)
        {
            DBManager.Instance.deleteDebit(idDebit);
        }

    }
}