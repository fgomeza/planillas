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
    public class ExtraRepository: Repository<ExtraEntity>
    {
        public ExtraRepository(AppContext context) : base(context){}

        public IEnumerable<ExtraEntity> selectExtrasByEmployee(long employeeId, DateTime endDate)
        {
            var extras = _context.Extras.Where((e)=>e.employeeId==employeeId && e.payrollId==null && e.date<=endDate);
            return extras.ToList();
        }

        public void assignPayroll(long payroll, long location, DateTime endDate)
        {
            _context.Extras.Where(e => e.payrollId == null && e.date<=endDate && e.fkextra_employee.locationId==location).ToList().ForEach(e => e.payrollId = payroll);
        }
    }
}
