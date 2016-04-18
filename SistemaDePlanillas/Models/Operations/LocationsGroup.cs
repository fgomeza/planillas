using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        /*
        public static Response add(User user, string name, double callPrice)
        {
            var result = DBManager.Instance.addLocation(name, callPrice);
            return Responses.Simple(result.Status);
        }
        */

        public static Response get(User user, long location_id)
        {
            var result = DBManager.Instance.getLocation(location_id);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get_all(User user) 
        {
            var result = DBManager.Instance.selectAllLocations();
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get_all_active(User user)
        {
            var result = DBManager.Instance.selectAllActiveLocations();
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response activate(User user, long id)
        {
            var result = DBManager.Instance.activateLocation(id);
            return Responses.Simple(result.Status);
        }

        public static Response modify(User user, long location_id, string name, double call_price)
        {
            var result = DBManager.Instance.updateLocation(user.Location, name, call_price);
            return Responses.Simple(result.Status);
        }

        public static Response set_Last(User user, long location_id, long last_payroll)
        {
            var result = DBManager.Instance.updateLocationLastPayroll(location_id, last_payroll);
            return Responses.Simple(result.Status);
        } 

        public static Response set_Current(User user, string name, long current_payroll,long administrator)
        {
            var result = DBManager.Instance.addLocation(name, current_payroll,administrator);
            return Responses.Simple(result.Status);
        }

        public static Response set_Administrator(User user, long  location,long administrator)
        {
            var result = DBManager.Instance.updateAdministrator(location,administrator);
            return Responses.Simple(result.Status);
        }

        public static Response remove(User user, long id)
        {
            var result = DBManager.Instance.deleteLocation(id);
            return Responses.Simple(result.Status);
        }
    }
}