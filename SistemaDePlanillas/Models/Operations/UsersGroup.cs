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
            var result=DBManager.Instance.addUser(name, username, password, role, user.Location, email);
            return Responses.Simple(result.Status);
        }

        public static Response get(User user, long id)
        {
            var result = DBManager.Instance.selectUser(id);
            return Responses.SimpleWithData(result.Status, result.Detail);
        }

        public static Response get(User user)
        {
            var result = DBManager.Instance.selectAllUsers(user.Location);
            return Responses.SimpleWithData(result.Status, result.Detail);
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