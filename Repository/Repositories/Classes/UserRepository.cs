using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Context;

namespace Repository.Repositories.Classes
{
    public class UserRepository: Repository<UserEntity>
    {
        public UserRepository(AppContext context) : base(context){}

        public IEnumerable<UserEntity> selectUsersByLocation(long locationId)
        {
            var users = _context.Users.Where(e => e.locationId == locationId);
            return users.ToList();
        }

        public UserEntity login(string username, string password)
        {
            var res = _context.Users.Where(u => u.userName == username && u.password == password).ToList();
            try { 
                return res.First();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
