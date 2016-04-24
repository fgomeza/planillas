using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class RolesGroup
    {
        public static object get(User user)
        {
            var roles = DBManager.Instance.selectAllRoles();
            return roles.Select(role => new { id = role.id, name = role.name, privileges = role.privileges });
        }

        public static object get_active(User user)
        {
            var roles = SessionManager.Instance.getRoles();
            return roles.Select(role => new { id = role.id, name = role.name, privileges = role.privileges });
        }

        public static void add(User user, string name, IEnumerable<object> operations)
        {
            DBManager.Instance.addRole(name, user.Location, operations.Select(o=>o.ToString()).ToList());
            SessionManager.Instance.updateRoles();
        }

        public static object get(User user, long id)
        {
            var role = SessionManager.Instance.getRole(id);
            return new { id = role.id, name = role.name, privileges = role.privileges };
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteRole(id);
            SessionManager.Instance.updateRoles();
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.activateRole(id);
            SessionManager.Instance.updateRoles();
        }

        public static void modify(User user, long id, string name, List<String> operations)
        {
            DBManager.Instance.updateRole(id, name, user.Location, operations);
            SessionManager.Instance.updateRoles();
        }

    }
}