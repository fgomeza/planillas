using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class GroupRepository : Repository<GroupEntity>
    {
        public GroupRepository(AppContext context):base(context){ }

        public GroupEntity Get(string id)
        {
            return _context.Set<GroupEntity>().Find(id);
        }
    }
}
