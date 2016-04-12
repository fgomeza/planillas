using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class LocationsGroup
    {
        public static Response add(User user, string name, double callPrice)
        {
            var result = DBManager.Instance.addLocation(name, callPrice);
            return Responses.Simple(result.Status);
        }

        public static Response get(User user) 
        {
            var result = DBManager.Instance.selectAllLocations();
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        /*
        public static string modify(User user, string name)
        {
            var result = DBManager.Instance.updateLocation(user.Location, name);
            return Responses.Simple(result.Status);
        }
        */

        public static Response remove(User user, long id)
        {
            var result = DBManager.Instance.deleteLocation(id);
            return Responses.Simple(result.Status);
        }
    }
}