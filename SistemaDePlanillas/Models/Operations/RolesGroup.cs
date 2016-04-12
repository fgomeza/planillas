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
            foreach (var role in roles)
            {
                result.Add(new { id = role.id, name = role.name, privileges = role.privileges });
            }
            return Responses.WithData(result);
        }


        public static string add(User user, string name, List<string> operations)
        {
            var result = DBManager.Instance.addRole(name, user.Location, operations);
            return Responses.Simple(result.Status);
        }


        public static string get(User user, long id)
        {
            try
            {
                var role = SessionManager.Instance.getRole(id);
                return Responses.WithData(new { id = role.id, name = role.name, privileges = role.privileges });
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.Instance.deleteRole(id);
            SessionManager.Instance.updateRoles();
            return Responses.Simple(result.Status);
        }

        public static string modify(User user, long id,string name, List<string> operations)
        {
            DBManager dbm = DBManager.Instance;
            var result = dbm.updateRole(id, name, user.Location, operations);
            return Responses.Simple(result.Status);
        }
    }
}