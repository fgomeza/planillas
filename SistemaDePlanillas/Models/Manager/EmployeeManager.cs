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
        public Employee addEmployee(string idCard, string locationName, string name, long location, string account, double salary, long avalaibleVacations, string CMS = null)
        {
            Employee result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, location=locationName,cms = CMS, name = name, locationId = location, active = true, account = account, salary=salary,workedDays=0, negativeAmount=0,  avalaibleVacations=avalaibleVacations};
                    var x = repository.Employees.Add(employee);
                    repository.Complete();
                    result = new Employee()
                    {
                        id = x.id,
                        idCard = x.idCard,
                        name = x.name,
                        location = x.locationId,
                        locationName = x.location,
                        account = x.account,
                        cmsText = CMS,
                        salary = x.salary,
                        active = x.active,
                        avalaibleVacations = x.avalaibleVacations,
                        activeSince = DateTime.Now                 
                    };
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void updateEmployeee(long id, string idCard, string locationName, string name, long location, string account, double salary, long avalaibleVacations, string CMS = null)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(id);
                    if (employee != null)
                    {
                        employee.location = locationName;
                        employee.idCard = idCard;
                        employee.cms = CMS;
                        employee.name = name;
                        employee.locationId = location;
                        employee.account = account;
                        employee.salary = salary;
                        employee.avalaibleVacations = avalaibleVacations;
                        repository.Complete();
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public List<Employee> selectAllEmployees(long location)
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.selectEmployees(location);
                    foreach (var x in employees)
                    {
                        var calls = repository.Calls.callsbyEmployee(x.id, DateTime.Now);

                        Employee employee = new Employee()
                        { id = x.id, idCard = x.idCard, name = x.name, locationName=x.location, location = x.locationId, account = x.account, cmsText = x.cms, calls = calls, active = x.active, activeSince=x.activeSince, salary=x.salary, avalaibleVacations=x.avalaibleVacations };
                        result.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        employee.activeSince = DateTime.Now;
                        repository.Complete();
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            cmsText = employee.cms,
                            account = employee.account,
                            salary = employee.salary,
                            active = employee.active,
                            negativeAmount = employee.negativeAmount,
                            activeSince = employee.activeSince,
                            calls = repository.Calls.callsbyEmployee(employee.id, DateTime.Now),
                            avalaibleVacations = employee.avalaibleVacations,
                            locationName = employee.location
                        };
                    }
                    else
                    {
                        throw validateException(employee != null ? App_LocalResoures.Errors.employeeInactive : App_LocalResoures.Errors.inexistentEmployee);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            calls = repository.Calls.callsbyEmployee(employee.id, DateTime.Now),
                            cmsText = employee.cms,
                            account = employee.account,
                            salary = employee.salary,
                            active = employee.active,
                            negativeAmount = employee.negativeAmount,
                            activeSince = employee.activeSince,
                            avalaibleVacations = employee.avalaibleVacations,
                            locationName = employee.location
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public Vacation addVacation(long employeeId,DateTime date, double price)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                   var vacation= repository.Vacations.Add(new VacationEntity()
                    {
                        employeeId=employeeId,
                        date=date,
                        vacationsPrice=price
                    });
                    return new Vacation()
                    {
                        employee = vacation.employeeId,
                        date = vacation.date,
                        vacationPrice = vacation.vacationsPrice
                    };
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public List<Vacation> selectVacations(long employeeId)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    return repository.Vacations.selectVacationsByEmployee(employeeId).Select(v=>new Vacation()
                    {
                        employee=v.employeeId,
                        date=v.date,
                        vacationPrice=v.vacationsPrice
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public List<Vacation> selectVacations(long employeeId, DateTime endDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    return repository.Vacations.selectVacationsByEmployee(employeeId,endDate).Select(v => new Vacation()
                    {
                        employee = v.employeeId,
                        date = v.date,
                        vacationPrice = v.vacationsPrice
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void removeVacation(long employeeId, DateTime date)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Vacations.Remove(repository.Vacations.Get(employeeId, date));
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void assingVacationsToPayroll(long payroll,long location,DateTime endDate)
        {

            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Vacations.assignPayroll(payroll,location, endDate);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

    }

}