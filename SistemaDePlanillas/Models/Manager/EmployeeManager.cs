using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class EmployeeManager : IErrors
    {
        public Employee addCmsEmployee(string idCard, string CMS, string name, long location, string account)
        {
            Employee result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, cms = CMS, iscms = true, name = name, locationId = location, active = true, account = account };
                    var x = repository.Employees.Add(employee);
                    repository.Complete();
                    result = new Employee()
                    {
                        id = x.id,
                        idCard = x.idCard,
                        name = x.name,
                        location = x.locationId,
                        account = x.account,
                        cms = false,
                        salary = x.salary,
                        active = x.active
                    };
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void updateCmsEmployeee(long id, string idCard, string CMS, string name, long location, string account)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(id);
                    if (employee != null)
                    {
                        employee.idCard = idCard;
                        employee.cms = CMS;
                        employee.name = name;
                        employee.locationId = location;
                        employee.account = account;
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

        public List<Employee> selectAllCmsEmployees(long location)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.selectCMSEmployees(location);
                    foreach (var x in employees)
                    {
                        var calls = repository.Calls.callsbyEmployee(x.id, DateTime.Now);

                        Employee employee = new Employee()
                        { id = x.id, idCard = x.idCard, name = x.name, location = x.locationId, account = x.account, cms = true, cmsText = x.cms, calls = calls, active = x.active };
                        result.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Call> callListByEmployee(long employee, DateTime endDate)
        {
            List<Call> result = new List<Call>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var calls = repository.Calls.callListbyEmployee(employee, endDate);
                    foreach (var call in calls)
                    {
                        result.Add(new Call() { employee = call.employeeId, calls = call.calls, date = call.date, hours = call.time });
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public Employee addNonCmsEmployee(string idCard, string name, long location, string account, float salary)
        {
            Employee result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, name = name, cms = " ", iscms = false, locationId = location, active = true, account = account, salary = salary };
                    var x = repository.Employees.Add(employee);
                    repository.Complete();
                    result = new Employee()
                    {
                        id = x.id,
                        idCard = x.idCard,
                        name = x.name,
                        location = x.locationId,
                        account = x.account,
                        cms = false,
                        salary = x.salary,
                        active = x.active
                    };
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void updateNonCmsEmployeee(long id, string idCard, string name, long location, string account, double salary)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(id);
                    if (employee != null)
                    {
                        employee.idCard = idCard;
                        employee.name = name;
                        employee.locationId = location;
                        employee.account = account;
                        employee.salary = salary;
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

        public List<Employee> selectAllNonCmsEmployees(long location)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.selectNonCMSEmployees(location);
                    foreach (var x in employees)
                    {
                        result.Add(new Employee()
                        {
                            id = x.id,
                            idCard = x.idCard,
                            name = x.name,
                            location = x.locationId,
                            account = x.account,
                            cms = false,
                            salary = x.salary,
                            active = x.active
                        });
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void deleteEmployee(long employeeId)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(employeeId);
                    if (employee != null)
                    {
                        employee.active = false;
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

        public void activateEmployee(long employeeId)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(employeeId);
                    if (employee != null)
                    {
                        employee.active = true;
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

        public Employee selectEmployee(long id)
        {
            Employee result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(id);

                    if (employee != null && employee.active)
                    {
                        result = new Employee()
                        {
                            id = employee.id,
                            idCard = employee.idCard,
                            location = employee.locationId,
                            name = employee.name,
                            cms = employee.cms == null ? false : true,
                            cmsText = employee.cms,
                            account = employee.account,
                            salary = employee.salary,
                            active = employee.active,
                            negativeAmount = employee.negativeAmount == null ? 0 : (double)employee.negativeAmount
                        };
                    }
                    else
                    {
                        validateException(employee != null ? App_LocalResoures.Errors.employeeInactive : App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Employee> selectAllActiveEmployees(long location)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.GetAll();
                    foreach (var employee in employees)
                    {
                        result.Add(new Employee()
                        {
                            id = employee.id,
                            idCard = employee.idCard,
                            location = employee.locationId,
                            name = employee.name,
                            cms = employee.cms == null ? false : true,
                            calls = employee.cms == null ? 0 : repository.Calls.callsbyEmployee(employee.id, DateTime.Now),
                            cmsText = employee.cms,
                            account = employee.account,
                            salary = employee.salary,
                            active = employee.active,
                            negativeAmount = employee.negativeAmount == null ? 0 : (double)employee.negativeAmount
                        });
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Employee> selectAllEmployees(long location)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.GetAll();
                    foreach (var employee in employees)
                    {
                        if (employee.active)
                        {
                            result.Add(new Employee()
                            {
                                id = employee.id,
                                idCard = employee.idCard,
                                location = employee.locationId,
                                name = employee.name,
                                cms = employee.iscms,
                                calls = employee.iscms ? repository.Calls.callsbyEmployee(employee.id, DateTime.Now) : 0,
                                cmsText = employee.cms,
                                account = employee.account,
                                salary = employee.salary,
                                active = employee.active,
                                negativeAmount = employee.negativeAmount
                            });
                        }
                    }
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