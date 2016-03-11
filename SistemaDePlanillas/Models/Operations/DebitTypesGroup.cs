using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitTypesGroup
    {
        public static string addType1(User user, string name)
        {
            try
            {
                var result = DBManager.Instance.addFixedDebitType(name,user.location);
                return Responses.Simple(result.status);
            }catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string addType2(User user, string name,float interestRate,long months)
        {
            try
            {
                var result = DBManager.Instance.addPaymentDebitType(name,interestRate,months,user.location);
                return Responses.Simple(result.status);
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
                return Responses.Simple(result.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string removeType1(User user, long id)
        {
            try
            {
                var result = DBManager.Instance.deleteFixedDebitType(id);
                return Responses.Simple(result.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string removeType2(User user, long id)
        {
            try
            {
                var result = DBManager.Instance.deletePaymentDebitType(id);
                return Responses.Simple(result.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string selectType1(User user)
        {
            try
            {
                var result = DBManager.Instance.selectFixedDebitTypes(user.location);
                return Responses.SimpleWithData(result.status,result.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string selectType2(User user)
        {
            try
            {
                var result = DBManager.Instance.selectPaymentDebitTypes(user.location);
                return Responses.SimpleWithData(result.status, result.detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
    }
}