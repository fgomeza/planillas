using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDePlanillas.Models;

namespace SistemaDePlanillas.Repository
{
    public class UserRepository : IUserRepository
    {
        /*
        private MVCEntities context;

        public UserRepository(MVCEntities context)
        {
            this.context = context;
        }

        */
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IUserRepository GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if(disposing)
                {
                    //context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}