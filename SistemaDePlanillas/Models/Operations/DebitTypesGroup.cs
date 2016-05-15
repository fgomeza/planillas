using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitTypesGroup
    {

        public static void add_Fixed(User user, string name)
        {
            DBManager.Instance.debits.addFixedDebitType(name, user.Location);
        }

        public static void add_Payment(User user, string name, double interestRate, long pays)
        {
            DBManager.Instance.debits.addPaymentDebitType(name, user.Location, pays, interestRate);
        }

        public static void add_Amortization(User user, string name, double interestRate, long pays)
        {
            DBManager.Instance.debits.addAmortizationDebitType(name, user.Location, pays, interestRate);
        }

        public static void modify_Fixed(User user, long id, string name)
        {
            DBManager.Instance.debits.updateFixedDebitType(id, name);
        }
        public static void modify_Payment(User user, long id, string name, long pays, double interestRate)
        {
            DBManager.Instance.debits.updatePaymentDebitType(id, name, interestRate, pays);
        }
        public static void modify(User user, long id, string name, long pays, double interestRate )
        {
            DBManager.Instance.debits.updateAmortizationDebitType(id, name, interestRate, pays);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.debits.deleteDebitType(id);
        }

        public static object get_FixedTypes(User user)
        {
            return DBManager.Instance.debits.selectFixedDebitTypes(user.Location);
        }

        public static object get_PaymentTypes(User user)
        {
            return DBManager.Instance.debits.selectPaymentDebitTypes(user.Location);
        }

        public static object get_AmortizationTypes(User user)
        {
            return DBManager.Instance.debits.selectAmortizationDebitTypes(user.Location);
        }

    }
}