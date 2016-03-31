using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        public static string add(User user, string name)
        {
            var result = DBManager.Instance.addLocation(name);
            return Responses.Simple(result.Status);
        }

        public static string get(User user) 
        {
            var result = DBManager.Instance.selectLocations();
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static string modify(User user, string name)
        {
            var result = DBManager.Instance.updateLocation(user.Location, name);
            return Responses.Simple(result.Status);
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.Instance.deleteLocation(id);
            return Responses.Simple(result.Status);
        }
    }
}