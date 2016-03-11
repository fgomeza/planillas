using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class UsersGroup
    {
        public static string add(User user, string name, string username, string password, string email, long role)
        {
            var result=DBManager.getInstance().addUser(name, username, password, role, user.location, email);
            return Responses.Simple(result.status);
        }

        public static string get(User user, long id)
        {
            var result = DBManager.getInstance().selectUser(id);
            return Responses.SimpleWithData(result.status, result.detail);
        }

        public static string get(User user)
        {
            var result = DBManager.getInstance().selectAllUsers(user.location);
            return Responses.SimpleWithData(result.status, result.detail);
        }

        public static string modify(User user, string name, string username, string password, string email, long role, long location)
        {
            var result = DBManager.getInstance().addUser(name, username, password, role, location, email);
            return Responses.Simple(result.status);
        }

        public static string remove(User user, long id)
        {
            var result = DBManager.getInstance().deleteUser(id);
            return Responses.Simple(result.status);
        }

        public static string root_get(User user) 
        {
            System.IO.StreamReader file = new System.IO.StreamReader("c:\\test.txt");
            return "{}";
        }

    }
}