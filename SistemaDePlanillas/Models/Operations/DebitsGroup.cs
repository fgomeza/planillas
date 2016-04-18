using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        
        public static Response add_Fixed(User user, long employee, string detail, long amount, long type)
        {
            try {
                var dataBaseResponse = DBManager.Instance.addDebit(employee, detail, amount, type);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static Response add_Payment(User user, long employee, string detail, double total, long type)
        {
            try {
                var dataBaseResponse = DBManager.Instance.addDebit(employee, detail, total, type);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static Response get_AllFixed(User user, long employee)
        {
            try
            {
                var dataBaseResponse = DBManager.Instance.selectDebits(employee);
                return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response get_AllPayment(User user, long employee)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectPaymentDebits(employee);
                return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e); 
            }
        }

        public static Response get_Fixed(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectDebit(idDebit);
                return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response get_Payment(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.selectPaymentDebit(idDebit);
                return Responses.SimpleWithData(dataBaseResponse.Status, dataBaseResponse.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response modify_Fixed(User user, long idDebit, string detail, long amount)
        {
            try {
                var dataBaseResponse = DBManager.Instance.updateDebit(idDebit, detail, amount);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response modify_Payment(User user, long idDebit,DateTime initialDate, string detail, float total, double interestRate, long months, double remainingAmount)
        {
            try {
                var dataBaseResponse = DBManager.Instance.updatePaymentDebit(idDebit, initialDate, detail, total, interestRate, months, remainingAmount);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response remove_Fixed(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.deleteDebit(idDebit);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static Response remove_Payment(User user, long idDebit)
        {
            try {
                var dataBaseResponse = DBManager.Instance.deleteDebit(idDebit);
                return Responses.Simple(dataBaseResponse.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        

    }
}