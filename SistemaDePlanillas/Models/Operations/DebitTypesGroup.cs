using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitTypesGroup
    {

        public static void add_FixedType(User user, string name)
        {
            DBManager.Instance.addDebitType(name, user.Location);
        }

        public static void add_PaymentType(User user, string name, double interestRate, long months)
        {
            DBManager.Instance.addDebitType(name, user.Location, months, interestRate);
        }

        public static void modify(User user, long id, string name, long months = 0, double interestRate = 0)
        {
            DBManager.Instance.updateDebitType(id, name, months, interestRate);
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
            return DBManager.Instance.selectNonFixedDebitTypes(user.Location);
        }

    }
}