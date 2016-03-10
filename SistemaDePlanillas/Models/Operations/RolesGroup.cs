using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class RolesGroup
    {
        public static string get(User user)
        {
            var roles = SessionManager.getInstance().getRoles();
            var result = new List<object>();
            foreach(var role in roles)
            {
                result.Add(new {id = role.id, name = role.name, privileges = role.privileges});
            }
            return Responses.WithData(result);
        }

        public static string add(User user, string name, string privileges)
        {
            string[] privsArray = privileges.Replace(" ", "").Split(new string[] { "," }, StringSplitOptions.None);
            try {
                var privsList = new List<Tuple<string, string>>();
                foreach (var priv in privsArray)
                {
                    string[] pair = priv.Split(new string[] { "/" }, StringSplitOptions.None);
                    privsList.Add(new Tuple<string,string>(pair[0], pair[1]));
                }
                var result = DBManager.getInstance().addRole(name, user.location, privsList);
                if (result.status == 0)
                {
                    //cargar nuevo rol en el session manager
                }
                return Responses.Simple(result.status);
            }
            catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string get(User user, long id)
        {
            try {
                var role = SessionManager.getInstance().getRole(id);
                return Responses.WithData(new { id = role.id, name = role.name, privileges = role.privileges });
            }
            catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string modify(User user, long id, string privileges)
        {
            string[] privsArray = privileges.Replace(" ", "").Split(new string[] { "," }, StringSplitOptions.None);
            try
            {
                var privsList = new List<Tuple<string, string>>();
                foreach (var priv in privsArray)
                {
                    string[] pair = priv.Split(new string[] { "/" }, StringSplitOptions.None);
                    privsList.Add(new Tuple<string, string>(pair[0], pair[1]));
                }
                //var result = DBManager.getInstance().addRole(name, user.location, privsList);
                //return Responses.Simple(result.status);
                return null;
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }
    }
}