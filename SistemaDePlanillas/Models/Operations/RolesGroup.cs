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
            var roles = SessionManager.Instance.getRoles();
            var result = new List<object>();
            foreach(var role in roles)
            {
                result.Add(new {id = role.id, name = role.name, privileges = role.privileges});
            }
            return Responses.WithData(result);
        }

        public static string add(User user, string name, IEnumerable<object> privileges)
        {
            try {
                var privsList = new List<Tuple<string, string>>();
                foreach (var priv in privileges)
                {
                    string[] pair = priv.ToString().Split(new string[] { "/" }, StringSplitOptions.None);
                    privsList.Add(new Tuple<string,string>(pair[0], pair[1]));
                }
                var result = DBManager.Instance.addRole(name, user.Location, privsList);
                if (result.status == 0)
                {
                    //cargar nuevo rol en el session manager
					SessionManager.Instance.updateRoles();
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
                var role = SessionManager.Instance.getRole(id);
                return Responses.WithData(new { id = role.id, name = role.name, privileges = role.privileges });
            }
            catch(Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.Instance.deleteRole(id);
            SessionManager.Instance.updateRoles();
            return Responses.Simple(result.status);
        }

        public static string modify(User user, long id,string name, IEnumerable<object> privs)
        {
            try
            {
                var privsList = new List<Tuple<string, string>>();
                foreach (var priv in privs)
                {
                    string[] pair = priv.ToString().Split(new string[] { "/" }, StringSplitOptions.None);
                    privsList.Add(new Tuple<string, string>(pair[0], pair[1]));
                }
                var result = DBManager.Instance.updateRole(id, name, privsList);
                SessionManager.Instance.updateRoles();
                return Responses.Simple(result.status);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

    }
}