using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        /*
        public static void add(User user, string name, double callPrice)
        {
            DBManager.Instance.addLocation(name, callPrice);
        }
        */

        public static object get(User user, long location_id)
        {
            return DBManager.Instance.getLocation(location_id);
        }

        public static object get_all(User user) 
        {
            return DBManager.Instance.selectAllLocations();
        }

        public static object get_all_active(User user)
        {
            return DBManager.Instance.selectAllActiveLocations();
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.activateLocation(id);
        }

        public static void modify(User user, long location_id, string name, double call_price)
        {
            DBManager.Instance.updateLocation(user.Location, name, call_price);
        }

        public static void set_Last(User user, long location_id, long last_payroll)
        {
            DBManager.Instance.updateLocationLastPayroll(location_id, last_payroll);
        } 

        public static void set_Current(User user, string name, long current_payroll,long administrator)
        {
            DBManager.Instance.addLocation(name, current_payroll,administrator);
        }

        public static void set_Administrator(User user, long  location,long administrator)
        {
            DBManager.Instance.updateAdministrator(location,administrator);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteLocation(id);
        }
    }
}