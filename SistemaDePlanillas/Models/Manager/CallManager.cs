using Repository.Context;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class CallManager : IErrors
    {
        public void addCall(String cms, long calls, TimeSpan hours, DateTime date)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employee = repository.Employees.selectEmployeeByCmsText(cms);
                    if (employee != null)
                    {
                        var call = repository.Calls.callByEmployeeDate(employee.id, date);
                        if (call != null)
                        {
                            call.calls = calls;
                            call.time = hours;
                        }
                        else
                        {
                            repository.Calls.Add(new CallEntity()
                            {
                                employeeId = employee.id,
                                calls = calls,
                                time = hours,
                                date = date,
                                payrollId = null
                            });
                        }
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void assignCallsToPayroll(long payrollId, DateTime endDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Calls.assignPayroll(payrollId, endDate);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }
    }
}