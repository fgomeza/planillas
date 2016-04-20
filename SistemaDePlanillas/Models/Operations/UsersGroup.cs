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
            var result = DBManager.Instance.addUser(name, username, password, role, user.Location, email);
            return Responses.Simple(result.Status);
        }

        public static Response get(User user, long id)
        {
            var result = DBManager.Instance.selectUser(id);
            if (result.Status == 0)
            {
                return Responses.WithData(formatUser(result.Detail));
            }
            return Responses.Error(result.Status);
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
            if (result.Status == 0)
            {
                return Responses.WithData(result.Detail.Select(u => formatUser(u)));
            }
            return Responses.Error(result.Status);
        }

        public static Response modify(User user, string name, string username, string password, string email, long role, long location)
        {
            var result = DBManager.Instance.addUser(name, username, password, role, location, email);
            return Responses.Simple(result.Status);
        }

        public static Response remove(User user, long id)
        {
            var result = DBManager.Instance.deleteUser(id);
            return Responses.Simple(result.Status);
        }

        public static Response root_get(User user)
        {
            var fileStream = new FileStream(@"c:\test.txt", FileMode.Open, FileAccess.Read);
            return Responses.WithData(FileConvertions.readFromCMSFile(fileStream));
        }
    }
}