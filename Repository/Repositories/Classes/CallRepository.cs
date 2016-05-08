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
    public class CallRepository : Repository<CallEntity>
    {

        public CallRepository(AppContext context) : base(context) {
        }

        public long callsbyEmployee(long employeeId , DateTime endDate)
        {
            return _context.Calls.Where((e) => e.employeeId == employeeId && e.payrollId==null && e.date <= endDate).Select(e => e.calls).ToList().Sum();
        }

        public IEnumerable<CallEntity> callListbyEmployee(long employeeId, DateTime endDate)
        {
            return _context.Calls.Where((e) => e.employeeId == employeeId && e.payrollId == null && e.date <= endDate);
        }

        public CallEntity callByEmployeeDate(long employee ,DateTime date)
        {
            return _context.Calls.FirstOrDefault(c=>c.employeeId==employee && c.date==date);
        }
    }
}
