using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitTypesGroup
    {
        /*
        public static string add_Type1(User user, string name)
        {
            try
            {
                var result = DBManager.Instance.addFixedDebitType(name,user.Location);
                return Responses.Simple(result.Status);
            }catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string add_Type2(User user, string name,float interestRate,long months)
        {
            try
            {
                var result = DBManager.Instance.addPaymentDebitType(name,interestRate,months,user.Location);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modify(User user, long id,string name,float interestRate,long months)
        {
            try
            {
                var result = DBManager.Instance.updatePaymentDebitType(id,name,interestRate,months);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove_Type1(User user, long id)
        {
            try
            {
                var result = DBManager.Instance.deleteFixedDebitType(id);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove_Type2(User user, long id)
        {
            try
            {
                var result = DBManager.Instance.deletePaymentDebitType(id);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string select_Type1(User user)
        {
            try
            {
                var result = DBManager.Instance.selectFixedDebitTypes(user.Location);
                return Responses.SimpleWithData(result.Status,result.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string select_Type2(User user)
        {
            try
            {
                var result = DBManager.Instance.selectPaymentDebitTypes(user.Location);
                return Responses.SimpleWithData(result.Status, result.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        */
    }
}