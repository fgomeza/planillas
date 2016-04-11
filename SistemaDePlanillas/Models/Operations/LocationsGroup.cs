using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        public static string add(User user, string name, double call_price)
        {
            var result = DBManager.Instance.addLocation(name, call_price);
            return Responses.Simple(result.Status);
        }

        public static string get(User user) 
        {
            var result = DBManager.Instance.selectAllLocations();
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static string modify(User user, string name, double call_price, long last_payroll, long current_payroll)
        {
            var result = DBManager.Instance.updateLocation(user.Location, name, call_price, last_payroll, current_payroll);
            return Responses.Simple(result.Status);
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.Instance.deleteLocation(id);
            return Responses.Simple(result.Status);
        }
    }
}