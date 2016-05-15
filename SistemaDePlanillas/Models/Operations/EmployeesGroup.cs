using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDePlanillas.Models;

namespace SistemaDePlanillas.Models.Operations
{
    public class EmployeesGroup
    {

        //employees/add/call
        public static void add_Call(User user, string cms, long calls, TimeSpan hours, DateTime date)
        {
            DBManager.Instance.calls.addCall(cms,calls,hours,date);
        }

        //employees/add/cms (string,string,string,string)
        public static Employee add(User user, string idCard, string idCMS, string name, string BCRAccount, string locationName, double salary, long vacations)
        {
            return DBManager.Instance.employees.addEmployee(idCard, locationName, name, user.Location, BCRAccount, salary, vacations, idCMS);
        }

        //employees/modify/cms (number, string,string,string,number,string)
        public static void modify(User user, long id, string idCard, string idCMS, string name, string BCRAccount, string locationName, double salary, long vacations)
        {
            DBManager.Instance.employees.updateEmployeee(id, idCard, locationName, name, user.Location, BCRAccount, salary, vacations, idCMS);
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

        public static object get_active(User user)
        {
            return DBManager.Instance.employees.selectAllActiveEmployees(user.Location);
        }
        //employees/get
        public static object get(User user)
        {
            return DBManager.Instance.employees.selectAllEmployees(user.Location);
        }

        public static object get(User user, long id)
        {
            return DBManager.Instance.employees.selectEmployee(id);
        }
    }
}