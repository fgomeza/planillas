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

        public IEnumerable<VacationEntity> selectVacationsByEmployee(long employee, DateTime endDate)
        {
            return _context.Vacations.Where(v => v.payrollId == null && v.employeeId == employee && v.date<=endDate).ToList();
        }

        public VacationEntity Get(long employee, DateTime date)
        {
            return _context.Vacations.Find(employee, date);
        }

        public void assignPayroll(long payroll, long location, DateTime endDate)
        {
            _context.Vacations.Where(v=>v.date<=endDate && v.fkvacation_employee.locationId==location).ToList().ForEach(e => e.payrollId = payroll);
        }
    }
}
