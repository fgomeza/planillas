using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitTypesGroup
    {

        public static Response add_FixedType(User user, string name)
        {
            var result = DBManager.Instance.addDebitType(name, user.Location);
            return Responses.Simple(result.Status);
        }

        public static Response add_PaymentType(User user, string name, double interestRate, long months)
        {
            var result = DBManager.Instance.addDebitType(name, user.Location, months, interestRate);
            return Responses.Simple(result.Status);
        }

        public static Response modify(User user, long id, string name, long months = 0, double interestRate = 0)
        {
            var result = DBManager.Instance.updateDebitType(id, name, months, interestRate);
            return Responses.Simple(result.Status);
        }

        public static Response remove(User user, long id)
        {
            var result = DBManager.Instance.deleteDebitType(id);
            return Responses.Simple(result.Status);
        }


        public static Response get_FixedTypes(User user)
        {
            var result = DBManager.Instance.selectFixedDebitTypes(user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get_PaymentTypes(User user)
        {
            var result = DBManager.Instance.selectNonFixedDebitTypes(user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

    }
}