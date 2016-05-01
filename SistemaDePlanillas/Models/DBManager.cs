using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using Repository.Repositories.Classes;
using Repository.Context;
using Repository.Entities;
using System.Collections;
using DevOne.Security.Cryptography.BCrypt;
using System.Linq;
using System.Resources;
using System.Reflection;

namespace SistemaDePlanillas.Models

{
    public class DBManager : IErrors
    {

        private static DBManager instance;

        public static DBManager Instance
        {
            get
            {
                return instance == null ? (instance = new DBManager()) : instance;
            }
        }
        public Employee addCmsEmployee(string idCard, string CMS, string name, long location, string account)
        {
            Employee result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, cms = CMS, name = name, locationId = location, active = true, account = account };
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
                    { idCard = idCard, name = name, locationId = location, active = true, account = account, salary = salary };
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
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void addCalls(List<FileConvertions.CMSRegister> calls)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    foreach (var call in calls)
                    {
                        repository.Calls.Add(new CallEntity()
                        {
                            employeeId = repository.Employees.selectEmployeeByCmsText(call.cmsid).id,
                            calls = call.calls,
                            time = call.hours,
                            date = call.date
                        });
                    }
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }


        public Debit addFixedDebit(long employee, string Detail, double amount, long type)
        {
            Debit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit=repository.Debits.Add(new DebitEntity()
                    {
                        employeeId = employee,
                        description = Detail,
                        totalAmount = amount,
                        debitTypeId = type,
                        active = true,
                    });
                    repository.Complete();
                    result = new Debit()
                    {
                        id = debit.id,
                        employee = debit.employeeId,
                        amount = debit.totalAmount,
                        detail = debit.description,
                        type = debit.debitTypeId,
                        typeName = debit.fkdebit_type.name
                    };
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public void updateFixedDebit(long idDebit, string Detail, double amount)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        debit.description = Detail;
                        debit.totalAmount = amount;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }

                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void deleteDebit(long idDebit)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null)
                    {
                        debit.active = false;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void activateDebit(long idDebit)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null)
                    {
                        debit.active = true;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public Debit selectFixedDebit(long idDebit)
        {
            Debit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        result = new Debit()
                        {
                            id = debit.id,
                            employee = debit.employeeId,
                            amount = debit.totalAmount,
                            detail = debit.description,
                            type = debit.debitTypeId,
                            typeName = debit.fkdebit_type.name
                        };
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Debit> selectActiveDebits(long employee)
        {
            List<Debit> result = new List<Debit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectFixedDebitsByEmployee(employee);
                    foreach (var debit in debits)
                    {
                        if (debit.active)
                        {
                            result.Add(new Debit()
                            {
                                id = debit.id,
                                amount = debit.totalAmount,
                                employee = debit.employeeId,
                                detail = debit.description,
                                type = debit.debitTypeId
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Debit> selectDebits(long employee)
        {
            List<Debit> result = new List<Debit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectFixedDebitsByEmployee(employee);
                    foreach (var debit in debits)
                    {
                        result.Add(new Debit()
                        {
                            id = debit.id,
                            amount = debit.totalAmount,
                            employee = debit.employeeId,
                            detail = debit.description,
                            type = debit.debitTypeId
                        });
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public PaymentDebit addPaymentDebit(long employee, DateTime initialDate, string Detail, double total, long pays, long type)
        {
            PaymentDebit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit = repository.Debits.Add(new DebitEntity()
                    {
                        initialDate = initialDate,
                        description = Detail,
                        employeeId = employee,
                        totalAmount = total,
                        remainingAmount = total,
                        paysMade = 0,
                        remainingPays = pays,
                        debitTypeId = type,
                        active = true
                    });
                    repository.Complete();
                    result = new PaymentDebit();
                    result.id = debit.id;
                    result.initialDate = debit.initialDate;
                    result.detail = debit.description;
                    result.employee = debit.employeeId;
                    result.total = debit.totalAmount;
                    result.remainingAmount = debit.remainingAmount;
                    result.paymentsMade = (long)debit.paysMade;
                    result.missingPayments = (long)debit.remainingPays;
                    result.interestRate = (double)debit.fkdebit_type.interestRate;
                    result.type = debit.debitTypeId;
                    result.typeName = debit.fkdebit_type.name;
                }

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public void updatePaymentDebit(long idDebit, double total, long remainingPays)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    var paid = debit.totalAmount - debit.remainingAmount;
                    if (debit != null && debit.active)
                    {
                        if (total < paid)
                            validateException(App_LocalResoures.Errors.negativeAmount);

                        debit.totalAmount = total;
                        debit.remainingAmount = debit.totalAmount - paid;
                        debit.remainingPays = remainingPays;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(debit!=null?App_LocalResoures.Errors.debitInactive: App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public PaymentDebit selectPaymentDebit(long idDebit)
        {
            PaymentDebit result = new PaymentDebit();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        result.id = debit.id;
                        result.initialDate = debit.initialDate;
                        result.detail = debit.description;
                        result.employee = debit.employeeId;
                        result.total = debit.totalAmount;
                        result.remainingAmount = debit.remainingAmount;
                        result.paymentsMade = (long)debit.paysMade;
                        result.missingPayments = (long)debit.remainingPays;
                        result.interestRate = (double)debit.fkdebit_type.interestRate;
                        result.type = debit.debitTypeId;
                        result.typeName = debit.fkdebit_type.name;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<PaymentDebit> selectPaymentDebits(long employee)
        {
            List<PaymentDebit> result = new List<PaymentDebit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectPaymentDebitsByEmployee(employee);
                    foreach (var debit in debits)
                    {
                        if (debit.active)
                        {
                            result.Add(new PaymentDebit()
                            {
                                id = debit.id,
                                initialDate = debit.initialDate,
                                detail = debit.description,
                                employee = debit.employeeId,
                                total = debit.totalAmount,
                                remainingAmount = debit.remainingAmount,
                                paymentsMade =  (long)debit.paysMade,
                                missingPayments =  (long)debit.remainingPays,
                                interestRate = (double)debit.fkdebit_type.interestRate,
                                type = debit.debitTypeId,
                                typeName = debit.fkdebit_type.name
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public void payDebit(long idDebit, float amount)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    repository.DebitPayments.Add(new DebitPaymentEntity()
                    {
                        debitId = idDebit,
                        Date = DateTime.Now,
                        Amount = amount,
                        RemainingAmount = debit.remainingAmount - amount
                    });
                    debit.remainingAmount = debit.remainingAmount - amount;
                    debit.remainingPays = debit.remainingPays - 1;
                    debit.paysMade = debit.paysMade + 1;
                    if (debit.remainingAmount == 0)
                        debit.active = false;

                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public AmortizationDebit addAmortizationDebit(long employee, DateTime initialDate, string Detail, double total, long pays, long type)
        {
            AmortizationDebit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit = repository.Debits.Add(new DebitEntity()
                    {
                        initialDate = initialDate,
                        description = Detail,
                        employeeId = employee,
                        totalAmount = total,
                        remainingAmount = total,
                        paysMade = 0,
                        remainingPays = pays,
                        debitTypeId = type,
                        active = true
                    });
                    repository.Complete();
                    result = new AmortizationDebit();
                    result.id = debit.id;
                    result.initialDate = debit.initialDate;
                    result.detail = debit.description;
                    result.employee = debit.employeeId;
                    result.total = debit.totalAmount;
                    result.remainingAmount = debit.remainingAmount;
                    result.paymentsMade = (long)debit.paysMade;
                    result.missingPayments = (long)debit.remainingPays;
                    result.interestRate = (double)debit.fkdebit_type.interestRate;
                    result.type = debit.debitTypeId;
                    result.typeName = debit.fkdebit_type.name;
                }

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public AmortizationDebit selectAmortizationDebit(long idDebit)
        {
            AmortizationDebit result = new AmortizationDebit();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        result.id = debit.id;
                        result.initialDate = debit.initialDate;
                        result.detail = debit.description;
                        result.employee = debit.employeeId;
                        result.total = debit.totalAmount;
                        result.remainingAmount = debit.remainingAmount;
                        result.paymentsMade = (long)debit.paysMade;
                        result.missingPayments = (long)debit.remainingPays;
                        result.interestRate = (double)debit.fkdebit_type.interestRate;
                        result.type = debit.debitTypeId;
                        result.typeName = debit.fkdebit_type.name;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<AmortizationDebit> selectAmortizationDebits(long employee)
        {
            List<AmortizationDebit> result = new List<AmortizationDebit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectAmortizationDebitsByEmployee(employee);
                    foreach (var debit in debits)
                    {
                        if (debit.active)
                        {
                            result.Add(new AmortizationDebit()
                            {
                                id = debit.id,
                                initialDate = debit.initialDate,
                                detail = debit.description,
                                employee = debit.employeeId,
                                total = debit.totalAmount,
                                remainingAmount = debit.remainingAmount,
                                paymentsMade =(long)debit.paysMade,
                                missingPayments =  (long)debit.remainingPays,
                                interestRate = (double)debit.fkdebit_type.interestRate,
                                type = debit.debitTypeId,
                                typeName = debit.fkdebit_type.name
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }
       

        public List<double> getLastSalaries(long employeeId)
        {
            List<double> result = new List<double>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var all = (List<SalaryEntity>)repository.Salarys.selectLastSalariesByEmployee(employeeId);
                    all.ForEach(s=>result.Add((double)s.salary));
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public Extra addExtra(long employee, string Detail, long hours)
        {
            Extra result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = new ExtraEntity()
                    {
                        employeeId = employee,
                        description = Detail,
                        hours = hours
                    };
                    extra = repository.Extras.Add(extra);
                    var rows = repository.Complete();
                    result = new Extra() { employee = extra.employeeId, detail = extra.description, hours = extra.hours, id = extra.id };
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void updateExtra(long idExtra, string Detail, long hours)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = repository.Extras.Get(idExtra);
                    if (extra != null)
                    {
                        extra.hours = (long)hours;
                        extra.description = Detail;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deleteExtra(long idExtra)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = repository.Extras.Get(idExtra);
                    if (extra != null)
                    {
                        repository.Extras.Remove(extra);
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }


        public Extra selectExtra(long idExtra)
        {
            Extra result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = repository.Extras.Get(idExtra);
                    if (extra != null)
                    {
                        result = new Extra()
                        {
                            hours = extra.hours,
                            detail = extra.description,
                            employee = extra.employeeId,
                            id = extra.id
                        };
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Extra> selectExtras(long employee)
        {
            List<Extra> result = new List<Extra>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extras = repository.Extras.selectExtrasByEmployee(employee);
                    foreach (var ex in extras)
                    {
                        result.Add(new Extra()
                        {
                            id = ex.id,
                            detail = ex.description,
                            hours = ex.hours,
                            employee = ex.employeeId
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

        public Penalty addPenalty(long employee, string Detail, long amount, long months, long penaltyTypeId, DateTime date)
        {
            Penalty result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalty = new PenaltyEntity()
                    {
                        Amount = amount,
                        Date = date,
                        Description = Detail,
                        EmployeeId = employee,
                        PenaltyTypeId = penaltyTypeId,
                        PenaltyPrice = repository.PenaltyTypes.getPriceById(penaltyTypeId),
                        active = true
                    };
                    penalty = repository.Penalties.Add(penalty);
                    repository.Complete();
                    result = new Penalty()
                    {
                        id = penalty.Id,
                        amount = penalty.Amount == null ? 0 : (double)penalty.Amount,
                        date = penalty.Date,
                        detail = penalty.Description,
                        employee = penalty.EmployeeId,
                        type = penalty.PenaltyTypeId,
                        typeName = penalty.fkpenalty_type.Name,
                        penaltyPrice = penalty.PenaltyPrice == null ? 0 : (double)penalty.PenaltyPrice
                    };
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void updatePenalty(long idRecess, long payroll, long penaltyTypeId, string Detail, long amount, DateTime date)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalty = repository.Penalties.Get(idRecess);
                    if (penalty != null)
                    {
                        penalty.PayRollId = payroll;
                        penalty.Description = Detail;
                        penalty.PenaltyTypeId = penaltyTypeId;
                        penalty.Amount = amount;
                        penalty.PenaltyPrice = repository.PenaltyTypes.getPriceById(penaltyTypeId);
                        penalty.Date = date;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deletePenalty(long idRecess)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);
                    if (penalty != null)
                    {
                        penalty.active = false;
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

        public void activatePenalty(long idRecess)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);
                    if (penalty != null)
                    {
                        penalty.active = true;
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

        public Penalty selectPenalty(long idRecess)
        {
            Penalty result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);

                    if (penalty != null && penalty.active)
                    {
                        result = new Penalty()
                        {
                            id = penalty.Id,
                            amount = penalty.Amount == null ? 0 : (double)penalty.Amount,
                            date = penalty.Date,
                            detail = penalty.Description,
                            employee = penalty.EmployeeId,
                            type = penalty.PenaltyTypeId,
                            typeName = penalty.fkpenalty_type.Name,
                            penaltyPrice = penalty.PenaltyPrice == null ? 0 : (double)penalty.PenaltyPrice
                        };
                    }
                    else
                    {
                        validateException(penalty != null ? App_LocalResoures.Errors.penaltyInactive : App_LocalResoures.Errors.inexistentPenalty);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Penalty> selectAllPenalty(long employee, DateTime endDate)
        {
            List<Penalty> result = new List<Penalty>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalties = repository.Penalties.selectPenaltiesByEmployee(employee, endDate);
                    foreach (var p in penalties)
                    {
                        result.Add(new Penalty()
                        {
                            id = p.Id,
                            amount = (double)p.Amount,
                            date = p.Date,
                            detail = p.Description,
                            employee = p.EmployeeId,
                            type = p.PenaltyTypeId,
                            typeName = p.fkpenalty_type.Name,
                            penaltyPrice = p.PenaltyPrice == null ? 0 : (double)p.PenaltyPrice
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

        public void payPenalty(long payrollId, long employeeId, DateTime endDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalties = repository.Penalties.selectPenaltiesByEmployee(employeeId, endDate);
                    foreach (PenaltyEntity p in penalties)
                    {
                        p.PayRollId = payrollId;
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public Dictionary<long, PenaltyType> selectAllPenaltyTypes(long location)
        {
            Dictionary<long, PenaltyType> result = new Dictionary<long, PenaltyType>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.PenaltyTypes.getAllbyLocation(location);
                    foreach (var p in types)
                    {
                        result.Add(p.Id, new PenaltyType() { id = p.Id, name = p.Name, price = p.Price });
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void addPayroll(DateTime date, long user, string file, long location)
        {
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void updatePayroll(long idPay, DateTime date, long user, string file)
        {
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void deletePayroll(long idPay)
        {
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public Payroll selectPayroll(long idPay)
        {
            Payroll result = new Payroll();
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Payroll> selectAllPayroll(long idPay)
        {
            List<Payroll> result = new List<Payroll>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Payroll> selectPayroll(DateTime ini, DateTime end)
        {
            List<Payroll> result = new List<Payroll>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public Location addLocation(string name, double call_price, long administrator)
        {
            Location result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = new LocationEntity()
                    { name = name, callPrice = call_price, active = true };
                    location = repository.Locations.Add(location);
                    repository.Complete();
                    updateAdministrator(location.id, administrator);
                    result = new Location();
                    result.Name = location.name;
                    result.CallPrice = location.callPrice == null ? 0 : (long)location.callPrice;
                    result.LastPayroll = location.lastPayrollId;
                    result.CurrentPayroll = location.currentPayrollId;
                    result.Active = location.active;
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public Location getLocation(long id)
        {
            Location result = new Location();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);

                    if (location != null && location.active)
                    {
                        result.Name = location.name;
                        result.CallPrice = location.callPrice == null ? 0 : (long)location.callPrice;
                        result.LastPayroll = location.lastPayrollId;
                        result.CurrentPayroll = location.currentPayrollId;
                        result.Active = location.active;
                    }
                    else
                    {
                        validateException(location != null ? App_LocalResoures.Errors.locationInactive : App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void activateLocation(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);

                    if (location != null && !location.active)
                    {
                        location.active = true;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(location != null ? App_LocalResoures.Errors.locationInactive : App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateAdministrator(long location, long administrator)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    AdministratorEntity admin = repository.Administrators.Get(location);
                    if (admin != null)
                    {
                        admin.user_id = administrator;
                    }
                    else
                    {
                        repository.Administrators.Add(new AdministratorEntity()
                        {
                            location = location,
                            user_id = administrator
                        });
                    }
                    repository.Complete();
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocation(long id, string name, double call_price)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null)
                    {
                        location.name = name;
                        location.callPrice = call_price;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocationLastPayroll(long id, long last_payroll)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null)
                    {
                        location.lastPayrollId = last_payroll;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocationCurrentPayroll(long id, long current_payroll)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null)
                    {
                        location.lastPayrollId = current_payroll;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deleteLocation(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null && location.active)
                    {
                        location.active = false;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public List<Location> selectAllLocations()
        {
            List<Location> result = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.GetAll();
                    foreach (var x in locations)
                    {
                        result.Add(new Location()
                        {
                            Id = x.id,
                            Name = x.name,
                            CallPrice = (double)x.callPrice
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

        public List<Location> selectAllActiveLocations()
        {
            List<Location> result = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.getAllActiveLocations();
                    foreach (var x in locations)
                    {
                        result.Add(new Location()
                        {
                            Id = x.id,
                            Name = x.name,
                            CallPrice = (double)x.callPrice
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

        public long addRole(string name, long location, List<string> operations)//**
        {
            long roleId = 0;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = new RoleEntity()
                    { name = name, locationId = location, active = true };
                    roleId = repository.Roles.Add(role).id;
                    foreach (var op in operations)
                    {
                        OperationEntity operation = repository.Operations.Get(op);
                        if (operation != null)
                        {
                            role.operations.Add(operation);
                        }
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return roleId;
        }

        public Role getRole(long id)
        {
            Role result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);

                    if (role != null && role.active == true)
                    {
                        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
                        foreach (var op in role.operations)
                        {
                            Tuple<string, string> tuple = new Tuple<string, string>(op.GroupId, op.Name.Split('/')[1]);
                            list.Add(tuple);
                        }
                        Role role_result = new Role(role.id, role.name, role.locationId, true, list);
                        result = role_result;
                    }
                    else
                    {
                        validateException(role != null ? App_LocalResoures.Errors.roleInactive : App_LocalResoures.Errors.inexistentRole);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void updateRole(long id, string name, long location, List<string> operations)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);
                    if (role != null)
                    {
                        role.name = name;
                        role.locationId = location;
                        role.operations.Clear();
                        foreach (var op in operations)
                        {
                            OperationEntity operation = repository.Operations.Get(op);
                            if (operation != null && role != null)
                            {
                                role.operations.Add(operation);
                            }
                        }
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentRole);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deleteRole(long id)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);
                    if (role != null)
                    {
                        var Users = selectAllActiveUsers(role.locationId);
                        if (Users.TrueForAll(u => u.Role != role.id))
                            role.active = false;
                        else
                            validateException(App_LocalResoures.Errors.roleWithUser);
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentRole);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void activateRole(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);
                    if (role != null)
                    {
                        role.active = true;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentRole);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public List<Role> selectAllRoles()
        {
            List<Role> result = new List<Role>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var roles = repository.Roles.GetAll();
                    foreach (var x in roles)
                    {
                        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
                        foreach (var op in x.operations)
                        {
                            Tuple<string, string> tuple = new Tuple<string, string>(op.GroupId, op.Name.Split('/')[1]);
                            list.Add(tuple);
                        }
                        result.Add(new Role(x.id, x.name, x.locationId, (bool)x.active, list));
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Role> selectAllActiveRoles()
        {
            List<Role> result = new List<Role>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var roles = repository.Roles.GetAll();
                    foreach (var x in roles)
                    {
                        if (x.active == true)
                        {
                            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
                            foreach (var op in x.operations)
                            {
                                Tuple<string, string> tuple = new Tuple<string, string>(op.GroupId, op.Name.Split('/')[1]);
                                list.Add(tuple);
                            }
                            result.Add(new Role(x.id, x.name, x.locationId, (bool)x.active, list));
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

        public void addOperation(string name, string description, string url, string group)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    OperationEntity operation = new OperationEntity()
                    { Name = name, Description = description, GroupId = group };
                    repository.Operations.Add(operation);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        private Operation getOperation(string id)
        {
            Operation result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var x = repository.Operations.Get(id);
                    if (x != null)
                    {
                        result = new Operation(x.Name, x.Name.Split('/')[1], x.GroupId, x.Description);
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        private void deleteOperation(string id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    OperationEntity operation = repository.Operations.Get(id);
                    if (operation != null)
                    {
                        repository.Operations.Remove(operation);
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public List<Operation> selectAllOperation()
        {
            List<Operation> result = new List<Operation>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var os = repository.Operations.GetAll();
                    foreach (var x in os)
                        result.Add(new Operation(x.Name,x.Name.Split('/')[1],x.GroupId,x.Description));
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Operation> selectAllOperationByGroup(String id_group)
        {
            List<Operation> result = new List<Operation>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var os = repository.Operations.selectOperationsByGroup(id_group);
                    foreach (var x in os)
                        result.Add(new Operation(x.Name, x.Name.Split('/')[1], x.GroupId, x.Description));
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void addOperationGroup(string name, string description, string icon)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    GroupEntity group = new GroupEntity()
                    { Name = name, Description = description, Icon = icon };
                    repository.Groups.Add(group);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        private OperationsGroup getOperationGroup(string id)
        {
            OperationsGroup result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    GroupEntity group = repository.Groups.Get(id);
                    if (group != null)
                    {
                        var operations = repository.Operations.selectOperationsByGroup(group.Name);
                        List<Operation> list = new List<Operation>();
                        foreach (var op in operations)
                            list.Add(new Operation(op.Name, op.Name.Split('/')[1], op.GroupId , op.Description));
                        result = new OperationsGroup(group.Description, group.Name, group.Icon, list);
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        private void deleteOperationGroup(string id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    GroupEntity group = repository.Groups.Get(id);
                    if (group != null)
                    {
                        repository.Groups.Remove(group);
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public List<OperationsGroup> selectAllOperationsGroup()
        {
            List<OperationsGroup> result = new List<OperationsGroup>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var os = repository.Groups.GetAll();
                    foreach (var x in os)
                    {
                        var operations = repository.Operations.selectOperationsByGroup(x.Name);
                        List<Operation> list = new List<Operation>();
                        foreach (var op in operations)
                            list.Add(new Operation(op.Name, op.Name.Split('/')[1], op.GroupId, op.Description));
                        result.Add(new OperationsGroup(x.Description, x.Name, x.Icon, list));
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }


        public User addUser(string name, string username, string password, long role, long location, string email)
        {
            User user = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var u = repository.Users.Add(new UserEntity()
                    {
                        name = name,
                        email = email,
                        locationId = location,
                        password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()),
                        roleId = role,
                        userName = username.ToLower(),
                        active = true
                    });
                    repository.Complete();
                    user = new User()
                    {
                        Id = u.id,
                        Name = u.name,
                        Username = u.userName,
                        Role = u.roleId,
                        Email = u.email,
                        Location = u.locationId,
                        Active = u.active
                    };
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return user;
        }

        public void updateUser(long id, string name, string username, long role, long location, string email)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        user.name = name;
                        user.userName = username;
                        user.roleId = role;
                        user.locationId = location;
                        user.email = email;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updatePassword(long id, string password)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deleteUser(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        user.active = false;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void activateUser(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        user.active = true;
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public User selectUser(long id)
        {
            User result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null && user.active == true)
                    {
                        result = new User()
                        {
                            Id = user.id,
                            Email = user.email,
                            Location = user.locationId,
                            Name = user.name,
                            Password = user.password,
                            Role = user.roleId,
                            Username = user.userName,
                            Active = true
                        };
                    }
                    else
                    {
                        validateException(user != null ? App_LocalResoures.Errors.userInactive : App_LocalResoures.Errors.inexistentUser);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<User> selectAllUsers()
        {
            List<User> result = new List<User>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var users = repository.Users.GetAll();
                    foreach (var ue in users)
                    {
                        result.Add(new User()
                        {
                            Id = ue.id,
                            Email = ue.email,
                            Location = ue.locationId,
                            Name = ue.name,
                            Password = ue.password,
                            Role = ue.roleId,
                            Username = ue.userName,
                            Active = (bool)ue.active
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

        public List<User> selectAllActiveUsers(long location)
        {
            List<User> result = new List<User>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var users = repository.Users.GetAll();
                    foreach (UserEntity ue in users)
                    {
                        if (ue.active == true)
                        {
                            result.Add(new User()
                            {
                                Id = ue.id,
                                Email = ue.email,
                                Location = ue.locationId,
                                Name = ue.name,
                                Password = ue.password,
                                Role = ue.roleId,
                                Username = ue.userName,
                                Active = true
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

        public User login(string username, string password)
        {
            User result = new User();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.login(username.ToLower(), password);
                    if (user != null)
                    {
                        result = new User() { Name = user.name, Id = user.id, Email = user.email, Location = user.locationId, Password = user.password, Role = user.roleId, Username = user.userName };
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Tuple<long, string>> selectAllErrors()
        {
            List<Tuple<long, string>> result = new List<Tuple<long, string>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var errors = repository.Errors.GetAll();
                    foreach (var error in errors)
                    {
                        result.Add(new Tuple<long, string>(error.id, error.message));
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public void addFixedDebitType(string name, long location)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                      repository.DebitTypes.Add(new DebitTypeEntity()
                        {
                            name = name,
                            locationId = location,
                            interestRate = 0,
                            type = "F"
                        });

                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void addPaymentDebitType(string name, long location, long pays, double interestRate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.DebitTypes.Add(new DebitTypeEntity()
                    {
                        name = name,
                        locationId = location,
                        pays= pays,
                        interestRate = interestRate,
                        type = "P"
                    });

                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void addAmortizationDebitType(string name, long location, long pays, double interestRate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    if (interestRate == 0)
                        validateException(App_LocalResoures.Errors.zeroInterestRate);
                    repository.DebitTypes.Add(new DebitTypeEntity()
                    {
                        name = name,
                        locationId = location,
                        pays = pays,
                        interestRate = interestRate,
                        type = "A"
                    });

                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void updateFixedDebitType(long id, string name)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                        type.name = name;
                   
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void updatePaymentDebitType(long id, string name, double interestRate, long pays)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    type.name = name;
                    type.interestRate = interestRate;
                    type.pays = pays;
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void updateAmortizationDebitType(long id, string name, double interestRate, long pays)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    type.name = name;
                    type.interestRate = interestRate;
                    type.pays = pays;
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public void deleteDebitType(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    repository.DebitTypes.Remove(type);
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
        }

        public List<DebitType> selectFixedDebitTypes(long location)
        {
            List<DebitType> result = new List<DebitType>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.DebitTypes.SelectByLocation(location);
                    foreach (var type in types)
                    {
                        if (type.type=="F")
                        {
                            result.Add(new DebitType()
                            {
                                id = type.id,
                                name = type.name,
                                location = type.locationId,
                                interestRate = type.interestRate,
                                payment = false
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<DebitType> selectPaymentDebitTypes(long location)
        {
            List<DebitType> result = new List<DebitType>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.DebitTypes.SelectByLocation(location);
                    foreach (var type in types)
                    {
                        if (type.type=="P")
                        {
                            result.Add(new DebitType()
                            {
                                id = type.id,
                                name = type.name,
                                location = type.locationId,
                                months = type.pays,
                                interestRate = type.interestRate,
                                payment = true
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }

        public List<DebitType> selectAmortizationDebitTypes(long location)
        {
            List<DebitType> result = new List<DebitType>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.DebitTypes.SelectByLocation(location);
                    foreach (var type in types)
                    {
                        if (type.type == "A")
                        {
                            result.Add(new DebitType()
                            {
                                id = type.id,
                                name = type.name,
                                location = type.locationId,
                                months = type.pays,
                                interestRate = type.interestRate,
                                payment = true
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
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
            catch (NpgsqlException e)
            {
                validateException(e);
            }
            return result;
        }
    }
}


