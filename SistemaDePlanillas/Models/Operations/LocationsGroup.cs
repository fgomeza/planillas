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
            var result = DBManager.getInstance().addLocation(name);
            return Responses.Simple(result.status);
        }

        public static string get(User user) 
        {
            var result = DBManager.getInstance().selectLocations();
            return Responses.SimpleWithData(result.status, result.detail);
        }

        public static string modify(User user, string name)
        {
            var result = DBManager.getInstance().updateLocation(user.location, name);
            return Responses.Simple(result.status);
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.getInstance().deleteLocation(id);
            return Responses.Simple(result.status);
        }
    }
}