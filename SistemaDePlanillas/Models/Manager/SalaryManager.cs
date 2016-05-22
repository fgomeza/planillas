using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class SalaryManager : IErrors
    {
        public List<Salary> getLastSalaries(long employeeId)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var salaries= repository.Salaries.selectLastSalariesByEmployee(employeeId);
                    return salaries.Select(s => new Salary()
                    { payroll = s.payrollId, employee = s.employeeId, salary = s.salary, netSalary = s.netSalary, workedDays = s.workedDays }).ToList();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void assignSalaryToPayroll(long employee,long payroll,double grossSalary, double netSalary)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Salaries.Add(new SalaryEntity()
                    {
                        employeeId=employee,
                        payrollId=payroll,
                        salary=grossSalary,
                        netSalary=netSalary
                    });
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public double selectSavingByEmployee(long employee)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var saving = repository.Savings.Get(employee);
                    return saving != null ? saving.amount : 0;
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }
    }
}