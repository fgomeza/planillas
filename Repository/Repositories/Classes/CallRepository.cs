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

        public long callsbyEmployee(long employeeId)
        {
            var x = _context.Calls.Where((e) => e.employeeId == employeeId && e.payrollId == null).Select(e => e.calls).Sum();

            return x;/*_context.Set<CallEntity>().Where((c) => c.employeeId == employeeId && c.payrollId==null).Sum((c)=>c.calls);*/

        }

        public IEnumerable<CallEntity> selectCallsByEmployee(long employeeId)
        {

            return _context.Calls.Where((e) => e.employeeId == employeeId).ToList();
        }
    }
}
