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
    public class ErrorRepository : Repository<ErrorEntity>
    {
        public ErrorRepository(AppContext context):base(context){}

        public ErrorEntity Get(string message)
        {
            return _context.Set<ErrorEntity>().Find(message);
        }
    }
}
