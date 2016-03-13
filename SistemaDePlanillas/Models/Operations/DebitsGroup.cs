using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {

        public static string add_Fixed(User user, long employee, string detail, long amount, long type)
        {
            try {
                var dataBaseResponse = DBManager.Instance.addDebit(employee, detail, amount, type);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string add_Payment(User user, long employee, string detail, double total, double interestRate, long months, long type)
        {
            try {
                var dataBaseResponse = DBManager.Instance.addPaymentDebit(employee, detail, total, interestRate, months, type);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static string get_AllFixed(User user, long employee)
        {
            try
            {
                var dataBaseResponse = DBManager.Instance.selectDebits(employee);
                return Responses.SimpleWithData(dataBaseResponse.status, dataBaseResponse.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string get_AllPayment(User user, long employee)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectPaymentDebits(employee);
                return Responses.SimpleWithData(dataBaseResponse.status, dataBaseResponse.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string get_Fixed(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectDebit(idDebit);
                return Responses.SimpleWithData(dataBaseResponse.status, dataBaseResponse.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string get_Payment(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectPaymentDebit(idDebit);
                return Responses.SimpleWithData(dataBaseResponse.status, dataBaseResponse.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modify_Fixed(User user, long idDebit, string detail, long amount)
        {
            try {
                var dataBaseResponse = DBManager.Instance.updateDebit(idDebit, detail, amount);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modify_Payment(User user, long idDebit, string detail, float total, double interestRate, long months, double remainingDebt)
        {
            try {
                var dataBaseResponse = DBManager.Instance.updatePaymentDebit(idDebit, detail, total, interestRate, months, remainingDebt);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove_Fixed(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.deleteDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove_Payment(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.deletePaymentDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

    }
}