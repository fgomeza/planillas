using SistemaDePlanillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IUserRepository GetUserById(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}