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
        public List<double> getLastSalaries(long employeeId)
        {
            List<double> result = new List<double>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var all = (List<SalaryEntity>)repository.Salaries.selectLastSalariesByEmployee(employeeId);
                    all.ForEach(s => result.Add((double)s.salary));
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
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
                validateException(e);
            }
        }

        public double selectSavingByEmployee(long employee)
        {
            double result = 0;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var saving = repository.Savings.Get(employee);
                    result = saving != null ? (double)saving.amount : 0;
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }
    }
}