using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static string addFixedDebit(User user, long employee, string detail, long amount, long type)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().addDebit(employee, detail, amount, type);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string addPaymentDebit(User user, long employee, string detail, double total, double interestRate, long months, long type)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().addPaymentDebit(employee, detail, total, interestRate, months, type);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static string getFixedDebits(User user, long employee)
        {
            try
            {
                var dataBaseResponse = DBManager.getInstance().selectDebits(employee);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string getPaymentDebits(User user, long employee)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().selectPaymentDebits(employee);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string getFixedDebit(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().selectDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string getPaymentDebit(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().selectPaymentDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modifyFixedDebit(User user, long idDebit, string detail, long amount)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().updateDebit(idDebit, detail, amount);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modifyPaymentDebit(User user, long idDebit, string detail, float total, double interestRate, long months, double remainingDebt)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().updatePaymentDebit(idDebit, detail, total, interestRate, months, remainingDebt);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string removeFixedDebit(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().deleteDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string removePaymentDebit(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.getInstance().deletePaymentDebit(idDebit);
                return Responses.Simple(dataBaseResponse.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string test(User user)
        {

            DBManager.getInstance().addExtra(5, "6565", 656.75f);
            return "fasfsa";
        }

    }
}