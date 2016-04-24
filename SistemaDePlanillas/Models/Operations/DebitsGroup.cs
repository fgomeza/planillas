using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static Response add_Fixed(User user, long employee, string detail, double amount, long type)
        {
            DBManager.Instance.addDebit(employee, detail, amount, type);
            return Responses.OK; 
        }

        public static Response add_Payment(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long months, long type)
        {

            DBManager.Instance.addPaymentDebit(employee, initialDate, detail, total, months, type);
            return Responses.OK;
        }

        public static Response get_AllFixed(User user, long employee)
        {
            var dataBaseResponse = DBManager.Instance.selectDebits(employee);
            return Responses.WithData(dataBaseResponse.Detail);
        }

        public static Response get_AllPayment(User user, long employee)
        {
            var dataBaseResponse = DBManager.Instance.selectPaymentDebits(employee);
            return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
        }

        public static Response get_Fixed(User user, long idDebit)
        {
            var dataBaseResponse = DBManager.Instance.selectDebit(idDebit);
            return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
        }

        public static Response get_Payment(User user, long idDebit)
        {
            var dataBaseResponse = DBManager.Instance.selectPaymentDebit(idDebit);
            return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
        }
    
        public static Response modify_Fixed(User user, long idDebit, string detail, double amount)
        {
            var dataBaseResponse = DBManager.Instance.updateDebit(idDebit, detail, amount);
            return Responses.Simple(dataBaseResponse.Status);
        }

        public static Response modify_Payment(User user, long idDebit, DateTime initialDate, string detail, double total, double interestRate, long months, double remainingAmount)
        {
            var dataBaseResponse = DBManager.Instance.updatePaymentDebit(idDebit, initialDate, detail, total, months, remainingAmount);
            return Responses.Simple(dataBaseResponse.Status);

        }

        public static Response remove_Fixed(User user, long idDebit)
        {
            var dataBaseResponse = DBManager.Instance.deleteDebit(idDebit);
            return Responses.Simple(dataBaseResponse.Status);
        }

        public static Response remove_Payment(User user, long idDebit)
        {
            var dataBaseResponse = DBManager.Instance.deleteDebit(idDebit);
            return Responses.Simple(dataBaseResponse.Status);
        }


    }
}