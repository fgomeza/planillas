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
    public class EmployeeRepository:Repository<EmployeeEntity>
    {

        public EmployeeRepository(AppContext context) : base(context)
        {}

        public IEnumerable<EmployeeEntity> selectCMSEmployees(long location)
        {
            var employees = _context.Employees.Where((e) => e.iscms && e.locationId==location && e.active);
            return employees.ToList();
        }

        public IEnumerable<EmployeeEntity> selectNonCMSEmployees(long location)
        {
            var employees = _context.Employees.Where((e) => !e.iscms && e.locationId == location && e.active);
            return employees.ToList();
        }

        public IEnumerable<EmployeeEntity> selectEmployeeByLocation(long locationId)
        {
            var employees = _context.Employees.Where((e) => e.locationId == locationId && e.active);
            return employees.ToList();
        }
        public EmployeeEntity selectEmployeeByCmsText(string cmsText)
        {
            var employee = _context.Employees.FirstOrDefault((e) => e.cms==cmsText && e.active);
            return employee;
        }




    }
}
