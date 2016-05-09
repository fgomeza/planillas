using SistemaDePlanillas.Models.Manager;
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
            return DBManager.Instance.locations.addLocation(name, callPrice,administrator);
        }
        

        public static Location get(User user, long location_id)
        {
            return DBManager.Instance.locations.getLocation(location_id);
        }

        public static List<Location> get(User user) 
        {
            return  DBManager.Instance.locations.selectAllLocations();
        }

        public static List<Location> get_active(User user)
        {
            return DBManager.Instance.locations.selectAllActiveLocations();
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.locations.activateLocation(id);
        }

        public static void modify(User user, long location_id, string name, double call_price)
        {
            DBManager.Instance.locations.updateLocation(user.Location, name, call_price);
        }

        public static void modify_Last(User user, long location_id)
        {
            DBManager.Instance.locations.updateLocationLastPayroll(location_id);
        } 

        public static void modify_Current(User user, string name, long current_payroll,long administrator)
        {
            DBManager.Instance.locations.addLocation(name, current_payroll,administrator);
        }

        public static void modify_Administrator(User user, long  location,long administrator)
        {
            DBManager.Instance.locations.updateAdministrator(location,administrator);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.locations.deleteLocation(id);
        }
    }
}