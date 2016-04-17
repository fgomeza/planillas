using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class EmployeesGroup
    {

        //employees/add/cms (string,string,string,string)
        public static Response add_CMS(User user, string idCard, string idCMS, string name, string BCRAccount)
        {
            var result = DBManager.Instance.addCmsEmployee(idCard, idCMS, name, (int)user.Location, BCRAccount);
            return Responses.Simple(result.Status);
        }

        //employees/add/cms (string,string,string,number,string)
        public static Response add_CMS(User user, string idCard, string idCMS, string name, long location, string BCRAccount)
        {
            var result = DBManager.Instance.addCmsEmployee(idCard, idCMS, name, (int)location, BCRAccount);
            return Responses.Simple(result.Status);
        }

        //employees/add/noncms (string,string,string,decimal)
        public static Response add_nonCMS(User user, string idCard, string name, string BCRAccount, double salary)
        {
            var result = DBManager.Instance.addNonCmsEmployee(idCard, name, (int)user.Location, BCRAccount, (float)salary);
            return Responses.Simple(result.Status);
        }

        //employees/add/noncms (string,string,number,string,decimal)
        public static Response add_nonCMS(User user, string idCard, string name, long location, string BCRAccount, double salary)
        {
            var result = DBManager.Instance.addNonCmsEmployee(idCard, name, (int)location, BCRAccount, (float)salary);
            return Responses.Simple(result.Status);
        }

        //employees/modify/noncms (number, string,string,string,number,string)
        public static Response modify_CMS(User user, long id, string idCard, string idCMS, string name, long location, string BCRAccount)
        {
            var result = DBManager.Instance.updateCmsEmployeee((int)id, idCard, idCMS, name, (int)location, BCRAccount);
            return Responses.Simple(result.Status);
        }

        //employees/modify/noncms (number, string,string,number,string,decimal)
        public static Response modify_nonCMS(User user, long id, string idCard, string name, long location, string BCRAccount, double salary)
        {
            var result = DBManager.Instance.updateNonCmsEmployeee((int)id, idCard, name, (int)location, BCRAccount, (float)salary);
            return Responses.Simple(result.Status);
        }

        //employees/remove (number)
        public static Response remove(User user, long id)
        {
            var result = DBManager.Instance.deleteEmployee((int)id);
            return Responses.Simple(result.Status);
        }

        public static Response activate(User user, long id)
        {
            var result = DBManager.Instance.activateEmployee((int)id);
            return Responses.Simple(result.Status);
        }

        //employees/get
        public static Response get(User user)
        {
            var result = DBManager.Instance.selectAllEmployees((int)user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get_active(User user)
        {
            var result = DBManager.Instance.selectAllActiveEmployees((int)user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }
        //employees/get/cms
        public static Response get_CMS(User user)
        {
            var result = DBManager.Instance.selectAllCmsEmployees((int)user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        //employees/get/noncms
        public static Response get_nonCMS(User user)
        {
            var result = DBManager.Instance.selectAllNonCmsEmployees((int)user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get(User user, long id)
        {
            var result = DBManager.Instance.selectEmployee((int)id);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }
    }
}