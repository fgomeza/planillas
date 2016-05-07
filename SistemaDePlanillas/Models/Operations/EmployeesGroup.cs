using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class EmployeesGroup
    {

        //employees/add/cms (string,string,string,string)
        public static Employee add_CMS(User user, string idCard, string idCMS, string name, string BCRAccount)
        {
            return DBManager.Instance.employees.addCmsEmployee(idCard, idCMS, name, (int)user.Location, BCRAccount);
        }


        //employees/add/noncms (string,string,string,decimal)
        public static Employee add_nonCMS(User user, string idCard, string name, string BCRAccount, double salary)
        {
            return DBManager.Instance.employees.addNonCmsEmployee(idCard, name, (int)user.Location, BCRAccount, (float)salary);
        }


        //employees/modify/noncms (number, string,string,string,number,string)
        public static void modify_CMS(User user, long id, string idCard, string idCMS, string name, long location, string BCRAccount)
        {
            DBManager.Instance.employees.updateCmsEmployeee((int)id, idCard, idCMS, name, (int)location, BCRAccount);
        }

        //employees/modify/noncms (number, string,string,number,string,decimal)
        public static void modify_nonCMS(User user, long id, string idCard, string name, long location, string BCRAccount, double salary)
        {
            DBManager.Instance.employees.updateNonCmsEmployeee(id, idCard, name, location, BCRAccount, salary);
        }

        //employees/remove (number)
        public static void remove(User user, long id)
        {
            DBManager.Instance.employees.deleteEmployee(id);
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.employees.activateEmployee(id);
        }

        //employees/get
        public static object get(User user)
        {
            return DBManager.Instance.employees.selectAllEmployees(user.Location);
        }

        public static object get_active(User user)
        {
            return DBManager.Instance.employees.selectAllActiveEmployees(user.Location);
        }
        //employees/get/cms
        public static object get_CMS(User user)
        {
            return DBManager.Instance.employees.selectAllCmsEmployees(user.Location);
        }

        //employees/get/noncms
        public static object get_nonCMS(User user)
        {
            return DBManager.Instance.employees.selectAllNonCmsEmployees(user.Location);
        }

        public static object get(User user, long id)
        {
            return DBManager.Instance.employees.selectEmployee(id);
        }
    }
}