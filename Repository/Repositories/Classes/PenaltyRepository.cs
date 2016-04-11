using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class PenaltyRepository : Repository<PenaltyEntity>
    {
        public PenaltyRepository(AppContext context) : base(context) { }

        public IEnumerable<PenaltyEntity> selectPenaltiesByEmployee(long employee)
        {
            return _context.Penalties.Where((p)=>p.EmployeeId==employee && p.PayRollId == null).ToList();
        }
    }
}
