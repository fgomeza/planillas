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
            DBManager.Instance.addFixedDebitType(name, user.Location);
        }

        public static void add_Payment(User user, string name, double interestRate, long pays)
        {
            DBManager.Instance.addPaymentDebitType(name, user.Location, pays, interestRate);
        }

        public static void add_Amortization(User user, string name, double interestRate, long pays)
        {
            DBManager.Instance.addAmortizationDebitType(name, user.Location, pays, interestRate);
        }

        public static void modify_Fixed(User user, long id, string name)
        {
            DBManager.Instance.updateFixedDebitType(id, name);
        }
        public static void modify_Payment(User user, long id, string name, long pays, double interestRate)
        {
            DBManager.Instance.updatePaymentDebitType(id, name, interestRate, pays);
        }
        public static void modify(User user, long id, string name, long pays, double interestRate )
        {
            DBManager.Instance.updateAmortizationDebitType(id, name, interestRate, pays);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteDebitType(id);
        }

        public static object get_FixedTypes(User user)
        {
            return DBManager.Instance.selectFixedDebitTypes(user.Location);
        }

        public static object get_PaymentTypes(User user)
        {
            return DBManager.Instance.selectPaymentDebitTypes(user.Location);
        }

        public static object get_AmortizationTypes(User user)
        {
            return DBManager.Instance.selectAmortizationDebitTypes(user.Location);
        }

    }
}