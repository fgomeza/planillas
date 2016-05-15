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

        public IEnumerable<PenaltyEntity> selectPenaltiesByEmployee(long employee, DateTime endDate)
        {
            return _context.Penalties.Where((p)=>p.EmployeeId==employee && p.payrollId == null && p.Date<=endDate ).ToList();
        }

        public void assignPayroll(long payroll,long location, DateTime endDate)
        {
            _context.Penalties.Where(c => c.payrollId == null && c.Date<=endDate && c.Date <= endDate).ToList().ForEach(c => c.payrollId = payroll);
        }
    }
}
