using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class UsersGroup
    {
        public static void add(User user, string name, string username, string password, string email, long role)
        {
            DBManager.Instance.addUser(name, username, password, role, user.Location, email);
        }

        public static object get(User user, long id)
        {
            return formatUser(DBManager.Instance.selectUser(id));
        }

        private static object formatUser(User user)
        {
            return new
            {
                name = user.Name,
                email = user.Email,
                username = user.Username,
                locationId = user.Location,
                locationName = DBManager.Instance.getLocation(user.Location).Name,
                roleId = user.Role,
                roleName = SessionManager.Instance.getRole(user.Role).name,
                active = user.Active,
                id = user.Id
            };
        }

        public static object get(User user)
        {
            var result = DBManager.Instance.selectAllUsers(user.Location);
            return Responses.WithData(result.Select(u => formatUser(u)));
        }

        public static object get_active(User user)
        {
            var result = DBManager.Instance.selectAllActiveUsers(user.Location);
            return Responses.WithData(result.Select(u => formatUser(u)));
        }

        public static void modify(User user, string name, string username, string password, string email, long role, long location)
        {
            DBManager.Instance.addUser(name, username, password, role, location, email);
        }

        public static void remove(User user, long id)
        {
            DBManager.Instance.deleteUser(id);
        }

        public static object root_get(User user)
        {
            var fileStream = new FileStream(@"c:\test.txt", FileMode.Open, FileAccess.Read);
            return Responses.WithData(FileConvertions.readFromCMSFile(fileStream));
        }
    }
}