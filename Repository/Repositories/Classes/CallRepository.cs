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
            return _context.Calls.Where((e) => e.employeeId == employeeId && e.payrollId==null && e.date <= endDate && e.fkcall_employee.active).Select(e => e.calls).ToList().Sum();
        }

        public IEnumerable<CallEntity> callListbyEmployee(long employeeId, DateTime endDate)
        {
            return _context.Calls.Where((e) => e.employeeId == employeeId && e.payrollId == null && e.date <= endDate && e.fkcall_employee.active);
        }

        public CallEntity callByEmployeeDate(long employee ,DateTime date)
        {
            return _context.Calls.FirstOrDefault(c=>c.employeeId==employee && c.date==date && c.fkcall_employee.active);
        }

        public void assignPayroll(long payroll, DateTime endDate)
        {
            _context.Calls.Where(c => c.payrollId == null && c.date <= endDate && c.fkcall_employee.active).ToList().ForEach(c => c.payrollId = payroll);
        }
    }
}
