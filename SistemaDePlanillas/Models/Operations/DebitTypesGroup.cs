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
            try
            {
                var result = DBManager.Instance.addDebitType(name, user.Location);
                return Responses.Simple(result.Status);
            }catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static Response add_PaymentType(User user, string name,float interestRate,long months)
        {
            try
            {
                var result = DBManager.Instance.addDebitType(name, user.Location, months, interestRate);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static Response modify(User user, long id,string name, long months=0,float interestRate=0)
        {
            try
            {
                var result = DBManager.Instance.updateDebitType(id, name, months, interestRate);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
        public static Response remove(User user, long id)
        {
            try
            {
                var result = DBManager.Instance.deleteDebitType(id);
                return Responses.Simple(result.Status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        

        public static Response get_FixedTypes(User user)
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

        public static Response get_PaymentTypes(User user)
        {
            try
            {
                var result = DBManager.Instance.selectNonFixedDebitTypes(user.Location);
                return Responses.SimpleWithData(result.Status, result.Detail);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
        
    }
}