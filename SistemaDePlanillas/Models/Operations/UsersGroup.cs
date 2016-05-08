using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class UsersGroup
    {
        public static object add(User user, string name, string username, string password, string email, long role)
        {
            return  formatUser(DBManager.Instance.users.addUser(name, username, password, role, user.Location, email));
        }

        public static object get(User user, long id)
        {
            return formatUser(DBManager.Instance.users.selectUser(id));
        }

        private static object formatUser(User user)
        {
            return new
            {
                name = user.Name,
                email = user.Email,
                username = user.Username,
                locationId = user.Location,
                locationName = DBManager.Instance.locations.getLocation(user.Location).Name,
                roleId = user.Role,
                roleName = SessionManager.Instance.getRole(user.Role).name,
                active = user.Active,
                id = user.Id
            };
        }

        public static object get(User user)
        {
            var result = DBManager.Instance.users.selectAllUsers().Where(u=>u.Location==user.Location);
            return result.Select(u => formatUser(u));
        }

        public static object get_active(User user)
        {
            var result = DBManager.Instance.users.selectAllActiveUsers(user.Location);
            return result.Select(u => formatUser(u));
        }

        public static void modify(User user, long id, string name, string username, string email, long role, long location)
        {
            DBManager.Instance.users.updateUser(id, name, username, role, location, email);
        }

        public static void modify_password(User user, long id,  string password)
        {
            DBManager.Instance.users.updatePassword(id,password);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.users.deleteUser(id);
        }

    }
}