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
            return DBManager.Instance.addDebit(employee, detail, amount, type);
        }

        public static PaymentDebit add_Payment(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long months, long type)
        {

            return DBManager.Instance.addPaymentDebit(employee, initialDate, detail, total, months, type);
        }

        public static object get_AllFixed(User user, long employee)
        {
            return DBManager.Instance.selectDebits(employee);
        }

        public static object get_AllPayment(User user, long employee)
        {
            return DBManager.Instance.selectPaymentDebits(employee);
        }

        public static object get_Fixed(User user, long idDebit)
        {
            return DBManager.Instance.selectDebit(idDebit);
        }

        public static object get_Payment(User user, long idDebit)
        {
            return DBManager.Instance.selectPaymentDebit(idDebit);  
        }
    
        public static void modify_Fixed(User user, long idDebit, string detail, double amount)
        {
            DBManager.Instance.updateDebit(idDebit, detail, amount);
        }

        public static void modify_Payment(User user, long idDebit, DateTime initialDate, string detail, double total, double interestRate, long months, double remainingAmount)
        {
            DBManager.Instance.updatePaymentDebit(idDebit, initialDate, detail, total, months, remainingAmount);
        }

        public static void remove_Fixed(User user, long idDebit)
        {
            DBManager.Instance.deleteDebit(idDebit);
        }

        public static void remove_Payment(User user, long idDebit)
        {
            DBManager.Instance.deleteDebit(idDebit);
        }

    }
}