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
    public class SalaryRepository:Repository<SalaryEntity>
    {
        public SalaryRepository(AppContext context) : base(context){}

        public IEnumerable<SalaryEntity> selectLastSalariesByEmployee(long employee)
        {
            return _context.Salaries.Where(s => s.employeeId == employee).OrderByDescending(s=>s.fksalary_payroll.endDate).Take(5).ToList();
        }
    }
}
