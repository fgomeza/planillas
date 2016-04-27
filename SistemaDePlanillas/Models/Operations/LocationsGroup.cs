using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        
        public static Location add(User user, string name, double callPrice,long administrator)
        {
            return DBManager.Instance.addLocation(name, callPrice,administrator);
        }
        

        public static Location get(User user, long location_id)
        {
            return DBManager.Instance.getLocation(location_id);
        }

        public static List<Location> get(User user) 
        {
            return  DBManager.Instance.selectAllLocations();
        }

        public static List<Location> get_active(User user)
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

        public static void modify_Last(User user, long location_id, long last_payroll)
        {
            DBManager.Instance.updateLocationLastPayroll(location_id, last_payroll);
        } 

        public static void modify_Current(User user, string name, long current_payroll,long administrator)
        {
            DBManager.Instance.addLocation(name, current_payroll,administrator);
        }

        public static void modify_Administrator(User user, long  location,long administrator)
        {
            DBManager.Instance.updateAdministrator(location,administrator);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteLocation(id);
        }
    }
}