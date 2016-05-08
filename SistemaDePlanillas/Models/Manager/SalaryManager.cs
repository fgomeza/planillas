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
                    var all = (List<SalaryEntity>)repository.Salarys.selectLastSalariesByEmployee(employeeId);
                    all.ForEach(s => result.Add((double)s.salary));
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