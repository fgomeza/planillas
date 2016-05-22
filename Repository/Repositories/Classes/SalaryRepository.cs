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
            var emp = _context.Employees.Find(employee);
            return _context.Salaries.Where(s => s.employeeId == employee && s.fksalary_payroll.initialDate>emp.activeSince && s.fksalary_payroll.initialDate>DateTime.Today.AddMonths(-6)).ToList();
        }

    }
}
