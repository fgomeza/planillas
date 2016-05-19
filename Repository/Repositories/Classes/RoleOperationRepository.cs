using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class RoleOperationRepository : Repository<RoleOperationEntity>
    {
        public RoleOperationRepository(AppContext context) : base(context) { }


        public IEnumerable<RoleOperationEntity> getOperationsByRole(long roleId)
        {
            return _context.RoleOperations.Where(x => x.role == roleId).ToList();
        }

        public void removeAllOperationByRole(long roleId)
        {
            var remove = getOperationsByRole(roleId);
            _context.RoleOperations.RemoveRange(remove);
        }
    }
}
