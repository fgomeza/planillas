using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class UsersGroup
    {
        public static Response add(User user, string name, string username, string password, string email, long role)
        {
            DBManager.Instance.addUser(name, username, password, role, user.Location, email);
            return Responses.OK;
        }

        public static Response get(User user, long id)
        {
            var result = DBManager.Instance.selectUser(id);
            return Responses.WithData(formatUser(result));
        }

        private static object formatUser(User user)
        {
            return new
            {
                name = user.Name,
                email = user.Email,
                username = user.Username,
                location = user.Location,
                role = user.Role,
                roleName = SessionManager.Instance.getRole(user.Role).name,
                active = user.Active,
                id = user.Id
            };
        }

        public static Response get(User user)
        {
            var result = DBManager.Instance.selectAllUsers(user.Location);
            return Responses.WithData(result.Select(u => formatUser(u)));
        }

        public static Response modify(User user, string name, string username, string password, string email, long role, long location)
        {
            DBManager.Instance.addUser(name, username, password, role, location, email);
            return Responses.OK;
        }

        public static Response remove(User user, long id)
        {
            DBManager.Instance.deleteUser(id);
            return Responses.OK;
        }

        public static Response root_get(User user)
        {
            var fileStream = new FileStream(@"c:\test.txt", FileMode.Open, FileAccess.Read);
            return Responses.WithData(FileConvertions.readFromCMSFile(fileStream));
        }
    }
}