using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class VacationRepository : Repository<VacationEntity>
    {
        public VacationRepository(AppContext context) : base(context) { }

        public IEnumerable<VacationEntity> selectVacationsByEmployee(long employee)
        {
            return _context.Vacations.Where(v => v.payrollId == null && v.employeeId==employee).ToList();
        }

        public void assignPayroll(long payroll, long location)
        {
            _context.Vacations.Where(v=>v.payrollId==null && v.fkvacation_employee.locationId==location).ToList().ForEach(e => e.payrollId = payroll);
        }
    }
}
