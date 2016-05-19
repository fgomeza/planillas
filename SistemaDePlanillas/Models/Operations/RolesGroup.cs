using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class RolesGroup
    {
        private static object formatRole(Role role, List<OperationsGroup> groups)
        {
            return new
            {
                id = role.id,
                name = role.name,
                groups = groups.Select(g => new
                {
                    id = g.name,
                    description = g.desc,
                    privileges = g.operations.Select(o => new
                    {
                        id = o.Id,
                        description = o.Description,
                        active = role.privileges.ContainsKey(g.name) && role.privileges[g.name].ContainsKey(o.Name)
                    })
                })
            };
        }

    

        public static object get(User user)
        {
            var roles = DBManager.Instance.roles.selectAllRoles().Where(r => r.location == user.Location);
            var groups = DBManager.Instance.groups.selectAllOperationsGroup();
            return roles.Select(r => formatRole(r, groups));
        }

        public static object get_active(User user)
        {
            var roles = DBManager.Instance.roles.selectAllActiveRoles().Where(r => r.location == user.Location);
            var groups = DBManager.Instance.groups.selectAllOperationsGroup();
            return roles.Select(r => formatRole(r, groups));
        }

        public static long add(User user, string name, IEnumerable<object> operations)
        {
            long roleId = DBManager.Instance.roles.addRole(name, user.Location, operations.Select(o => o.ToString()).ToList());
            SessionManager.Instance.updateRoles();
            return roleId;
        }

        public static object get(User user, long id)
        {
            var role = DBManager.Instance.roles.getRole(id);
            var groups = DBManager.Instance.groups.selectAllOperationsGroup();
            return formatRole(role, groups);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.roles.deleteRole(id);
            SessionManager.Instance.updateRoles();
        }

        public static void activate(User user, long id)
        {
            DBManager.Instance.roles.activateRole(id);
            SessionManager.Instance.updateRoles();
        }

        public static void modify(User user, long id, string name, List<string> operations)
        {
            DBManager.Instance.roles.updateRole(id, name, user.Location, operations);
            SessionManager.Instance.updateRoles();
        }

        public static object get_groups(User user)
        {
            var groups = DBManager.Instance.groups.selectAllOperationsGroup();
            return new
            {
                groups = groups.Select(g => new
                {
                    id = g.name,
                    description = g.desc,
                    privileges = g.operations.Select(o => new
                    {
                        id = o.Id,
                        description = o.Description,
                        active = false
                    })
                })
            };
        }

    }
}