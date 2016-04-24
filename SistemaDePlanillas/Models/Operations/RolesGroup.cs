using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class RolesGroup
    {
        public static Response get(User user)
        {
            var roles = SessionManager.Instance.getRoles();
            var result = roles.Select(role => new { id = role.id, name = role.name, privileges = role.privileges });
            return Responses.WithData(result);
        }


        public static Response add(User user, string name, IEnumerable<object> operations)
        {
            DBManager.Instance.addRole(name, user.Location, operations.Select(o=>o.ToString()).ToList());
            return Responses.OK;
        }


        public static Response get(User user, long id)
        {
            var role = SessionManager.Instance.getRole(id);
            return Responses.WithData(new { id = role.id, name = role.name, privileges = role.privileges });
        }

        public static Response remove(User user, long id)
        {
            DBManager.Instance.deleteRole(id);
            SessionManager.Instance.updateRoles();
            return Responses.OK;
        }

        public static Response activate(User user, long id)
        {
            DBManager.Instance.activateRole(id);
            return Responses.OK;
        }

        public static Response modify(User user, long id, string name, List<String> operations)
        {
            DBManager.Instance.updateRole(id, name, user.Location, operations);
            return Responses.OK;
        }

        public static Response get_all(User user)
        {
            var result = DBManager.Instance.selectAllRoles();
            return Responses.WithData(result);
        }

        public static Response get_active(User user)
        {
            var result = DBManager.Instance.selectAllActiveRoles();
            return Responses.WithData(result);
        }
    }
}