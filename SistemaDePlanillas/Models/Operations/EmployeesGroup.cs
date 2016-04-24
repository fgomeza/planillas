using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class EmployeesGroup
    {

        //employees/add/cms (string,string,string,string)
        public static void add_CMS(User user, string idCard, string idCMS, string name, string BCRAccount)
        {
            DBManager.Instance.addCmsEmployee(idCard, idCMS, name, (int)user.Location, BCRAccount);
        }


        //employees/add/noncms (string,string,string,decimal)
        public static void add_nonCMS(User user, string idCard, string name, string BCRAccount, double salary)
        {
            DBManager.Instance.addNonCmsEmployee(idCard, name, (int)user.Location, BCRAccount, (float)salary);
        }


        //employees/modify/noncms (number, string,string,string,number,string)
        public static void modify_CMS(User user, long id, string idCard, string idCMS, string name, long location, string BCRAccount)
        {
            DBManager.Instance.updateCmsEmployeee((int)id, idCard, idCMS, name, (int)location, BCRAccount);
        }

        //employees/modify/noncms (number, string,string,number,string,decimal)
        public static void modify_nonCMS(User user, long id, string idCard, string name, long location, string BCRAccount, double salary)
        {
            DBManager.Instance.updateNonCmsEmployeee(id, idCard, name, location, BCRAccount, salary);
        }

        //employees/remove (number)
        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteEmployee(id);
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.activateEmployee(id);
        }

        //employees/get
        public static object get_all(User user)
        {
            var result = DBManager.Instance.selectAllEmployees(user.Location);
            return Responses.WithData(result);
        }

        public static object get(User user)
        {
            var result = DBManager.Instance.selectAllActiveEmployees(user.Location);
            return Responses.WithData(result);
        }
        //employees/get/cms
        public static object get_CMS(User user)
        {
            var result = DBManager.Instance.selectAllCmsEmployees(user.Location);
            return Responses.WithData(result);
        }

        //employees/get/noncms
        public static object get_nonCMS(User user)
        {
            var result = DBManager.Instance.selectAllNonCmsEmployees(user.Location);
            return Responses.WithData(result);
        }

        public static object get(User user, long id)
        {
            return DBManager.Instance.selectEmployee(id);
        }
    }
}