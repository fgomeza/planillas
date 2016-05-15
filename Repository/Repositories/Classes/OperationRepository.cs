using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Context;
using Repository.Entities;

namespace Repository.Repositories.Classes
{
    public class OperationRepository : Repository<OperationEntity>
    {

    public OperationRepository(AppContext context) : base(context){}

        public OperationEntity Get(string id)
        {
            return _context.Operations.Find(id);
        }

        public IEnumerable<OperationEntity> selectOperationsByGroup(string id_group)
        {
            var operations = _context.Operations.Where((e) => e.GroupId == id_group);
            return operations.ToList();
        }
    }
}
