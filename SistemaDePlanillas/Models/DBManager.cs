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

namespace SistemaDePlanillas.Models

{
    public class DBManager : IErrors
    {
        private Dictionary<string, long> errors;

        private static DBManager instance;


        public static DBManager Instance
        {
            get
            {
                return instance == null ? (instance = new DBManager()) : instance;
            }
        }

        private long validate(Exception e)
        {
            if (e.InnerException != null)
            {
                e = e.InnerException;
                if (e.InnerException != null && e.InnerException is NpgsqlException)
                {
                    return 1;
                    //return errors[(e.InnerException as NpgsqlException).ConstraintName];
                }
            }
            throw e;
        }


        public Result<string> addCmsEmployee(string idCard, string CMS, string name, long location, string account)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, cms = CMS, name = name, locationId = location, active = true, account = account };
                    repository.Employees.Add(employee);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateCmsEmployeee(long id, string idCard, string CMS, string name, long location, string account)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentEmployee;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Employee>> selectAllCmsEmployees(long location)
        {
            Result<List<Employee>> result = new Result<List<Employee>>();
            result.Detail = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.selectCMSEmployees(location);
                    foreach (var x in employees)
                    {
                        var calls = repository.Calls.callsbyEmployee(x.id);

                        Employee employee = new Employee()
                        { id = x.id, idCard = x.idCard, name = x.name, location = x.locationId, account = x.account, cms = true, cmsText = x.cms, calls = calls };
                        result.Detail.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addNonCmsEmployee(string idCard, string name, long location, string account, float salary)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    { idCard = idCard, name = name, locationId = location, active = true, account = account, salary = salary };
                    repository.Employees.Add(employee);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> updateNonCmsEmployeee(long id, string idCard, string name, long location, string account, float salary)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentEmployee;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Employee>> selectAllNonCmsEmployees(long location)
        {
            Result<List<Employee>> result = new Result<List<Employee>>();
            result.Detail = new List<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.selectNonCMSEmployees(location);
                    foreach (var x in employees)
                    {
                        Employee employee = new Employee()
                        { id = x.id, idCard = x.idCard, name = x.name, location = x.locationId, account = x.account, cms = false, salary = x.salary };
                        result.Detail.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> deleteEmployee(long employeeId)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentEmployee;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Employee> selectEmployee(long id)
        {
            Result<Employee> result = new Result<Employee>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    EmployeeEntity employee = repository.Employees.Get(id);

                    if (employee != null && employee.active)
                    {
                        Employee res = new Employee();
                        res.id = employee.id;
                        res.idCard = employee.idCard;
                        res.location = employee.locationId;
                        res.name = employee.name;
                        res.cms = employee.cms == null ? false : true;
                        res.cmsText = employee.cms;
                        res.account = employee.account;
                        res.salary = employee.salary;
                        result.Detail = res;
                    }
                    else
                    {
                        result.Status = inexistentEmployee;
                    }

                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Employee>> selectAllEmployees(long location)
        {
            Result<List<Employee>> result = new Result<List<Employee>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var employees = repository.Employees.GetAll();
                    result.Detail = new List<Employee>();
                    foreach (var employee in employees)
                    {
                        if (employee.active)
                        {
                            Employee res = new Employee();
                            res.id = employee.id;
                            res.idCard = employee.idCard;
                            res.location = employee.locationId;
                            res.name = employee.name;
                            res.cms = employee.cms == null ? false : true;
                            res.cmsText = employee.cms;
                            res.account = employee.account;
                            res.salary = employee.salary;
                            result.Detail.Add(res);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addDebit(long employee, string Detail, double amount, long type)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Debits.Add(new DebitEntity()
                    {
                        employeeId = employee,
                        description = Detail,
                        totalAmount = amount,
                        debitTypeId = type,
                        interestRate = 0,
                        active = true,
                        payment = false
                    });
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> updateDebit(long idDebit, string Detail, double amount)
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
                        result.Status = inexistentDebit;
                    }

                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> deleteDebit(long idDebit)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentDebit;
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Debit> selectDebit(long idDebit)
        {
            Result<Debit> result = new Result<Debit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        Debit res = new Debit();
                        res.id = debit.id;
                        res.employee = debit.employeeId;
                        res.amount = debit.totalAmount;
                        res.detail = debit.description;
                        res.type = debit.debitTypeId;
                        result.Detail = res;
                    }
                    else
                    {
                        result.Status = inexistentDebit;
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Debit>> selectDebits(long employee)
        {
            Result<List<Debit>> result = new Result<List<Debit>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectFixDebitsByEmployee(employee);
                    result.Detail = new List<Debit>();
                    foreach (var debit in debits)
                    {
                        if (debit.active)
                        {
                            Debit deb = new Debit();
                            deb.id = debit.id;
                            deb.amount = debit.totalAmount;
                            deb.employee = debit.employeeId;
                            deb.detail = debit.description;
                            deb.type = debit.debitTypeId;
                            result.Detail.Add(deb);
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addPaymentDebit(long employee, DateTime initialDate, string Detail, double total, double interestRate, long months, long type)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Debits.Add(new DebitEntity()
                    {
                        initialDate = initialDate,
                        description = Detail,
                        employeeId = employee,
                        totalAmount = total,
                        remainingAmount = total,
                        paidMonths = 0,
                        remainingMonths = months,
                        interestRate = interestRate,
                        debitTypeId = type,
                        active = true,
                        payment = true
                    });
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> updatePaymentDebit(long idDebit, DateTime initialDate, string Detail, float total, double interestRate, long months, double remainingAmount)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        debit.initialDate = initialDate;
                        debit.description = Detail;
                        debit.totalAmount = total;
                        debit.remainingAmount = remainingAmount;
                        debit.remainingMonths = months - debit.paidMonths;
                        debit.interestRate = interestRate;
                        repository.Complete();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }

            return result;
        }

        public Result<PaymentDebit> selectPaymentDebit(long idDebit)
        {
            Result<PaymentDebit> result = new Result<PaymentDebit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitEntity debit = repository.Debits.Get(idDebit);
                    if (debit != null && debit.active)
                    {
                        PaymentDebit deb = new PaymentDebit();
                        deb.id = debit.id;
                        deb.initialDate = debit.initialDate;
                        deb.detail = debit.description;
                        deb.employee = debit.employeeId;
                        deb.total = debit.totalAmount;
                        deb.remainingAmount = debit.remainingAmount;
                        deb.paymentsMade = debit.paidMonths;
                        deb.missingPayments = debit.remainingMonths;
                        deb.interestRate = debit.interestRate;
                        deb.type = debit.debitTypeId;
                    }
                    else
                    {
                        result.Status = inexistentDebit;
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<PaymentDebit>> selectPaymentDebits(long employee)
        {
            Result<List<PaymentDebit>> result = new Result<List<PaymentDebit>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectDebitsNonFixByEmployee(employee);
                    result.Detail = new List<PaymentDebit>();
                    foreach (var debit in debits)
                    {
                        if (debit.active)
                        {
                            PaymentDebit deb = new PaymentDebit();
                            deb.id = debit.id;
                            deb.initialDate = debit.initialDate;
                            deb.detail = debit.description;
                            deb.employee = debit.employeeId;
                            deb.total = debit.totalAmount;
                            deb.remainingAmount = debit.remainingAmount;
                            deb.paymentsMade = debit.paidMonths;
                            deb.missingPayments = debit.remainingMonths;
                            deb.interestRate = debit.interestRate;
                            deb.type = debit.debitTypeId;
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<String> payDebit(long idDebit, float amount)
        {
            Result<string> result = new Result<string>();
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
                    debit.remainingMonths = debit.remainingMonths - 1;
                    debit.paidMonths = debit.paidMonths + 1;
                    if (debit.remainingAmount == 0)
                        debit.active = false;

                    repository.Complete();
                }

            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addExtra(long employee, string Detail, float amount)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = new ExtraEntity()
                    {
                        employeeId = employee,
                        description = Detail,
                        amount = amount
                    };
                    repository.Extras.Add(extra);
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> updateExtra(long idExtra, string Detail, float amount)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = repository.Extras.Get(idExtra);
                    if (extra != null)
                    {
                        extra.amount = (double)amount;
                        extra.description = Detail;
                    }
                    else
                    {
                        result.Status = inexistentExtra;
                    }
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> deleteExtra(long idExtra)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentExtra;
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }


        public Result<Extra> selectExtra(long idExtra)
        {
            Result<Extra> result = new Result<Extra>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extra = repository.Extras.Get(idExtra);
                    if (extra != null)
                    {
                        result.Detail.amount = extra.amount;
                        result.Detail.detail = extra.description;
                        result.Detail.employee = extra.employeeId;
                        result.Detail.id = extra.id;
                    }
                    else
                    {
                        result.Status = inexistentExtra;
                    }

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<List<Extra>> selectExtras(long employee)
        {
            Result<List<Extra>> result = new Result<List<Extra>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extras = repository.Extras.selectExtrasByEmployee(employee);
                    foreach (var ex in extras)
                    {
                        result.Detail.Add(new Extra()
                        {
                            id = ex.id,
                            detail = ex.description,
                            amount = ex.amount,
                            employee = ex.employeeId
                        });
                    }
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> addPenalty(long employee, string Detail, float amount, long months, long payRollId, long penaltyTypeId, float penaltyPrice, DateTime date)
        {
            Result<string> result = new Result<string>();
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
                        PenaltyPrice = repository.PenaltyTypes.getPriceById(penaltyTypeId)
                    };
                    repository.Penalties.Add(penalty);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> updatePenalty(long idRecess, long penaltyTypeId, string Detail, double amount, float penaltyPrice, DateTime date)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalty = repository.Penalties.Get(idRecess);
                    if (penalty != null)
                    {
                        penalty.Description = Detail;
                        penalty.PenaltyTypeId = penaltyTypeId;
                        penalty.Amount = amount;
                        penalty.PenaltyPrice = penaltyPrice;
                        penalty.Date = date;
                    }
                    else
                    {
                        result.Status = inexistentExtra;
                    }
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> deletePenalty(long idRecess)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);
                    if (penalty != null)
                    {
                        repository.Penalties.Remove(penalty);
                        repository.Complete();
                    }
                    else
                    {
                        result.Status = inexistentEmployee;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Penalty> selectPenalties(long idRecess)
        {
            Result<Penalty> result = new Result<Penalty>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);

                    if (penalty != null)
                    {
                        result.Detail = new Penalty()
                        {
                            id = penalty.Id,
                            amount = penalty.Amount,
                            detail = penalty.Description,
                            employee = penalty.EmployeeId

                        };
                    }
                    else
                    {
                        result.Status = inexistentEmployee;
                    }

                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Penalty>> selectAllPenalty(long employee)
        {
            Result<List<Penalty>> result = new Result<List<Penalty>>();
            result.Detail = new List<Penalty>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalties = repository.Penalties.selectPenaltiesByEmployee(employee);
                    foreach (PenaltyEntity p in penalties)
                    {
                        result.Detail.Add(new Penalty()
                        {
                            amount = (double)p.Amount,
                            detail = p.Description,
                            employee = p.EmployeeId,
                            id = p.Id
                        });
                    }
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }


        public Result<String> payPenalty(long payrollId, long employeeId)
        {
            Result<String> result = new Result<String>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var penalties = repository.Penalties.selectPenaltiesByEmployee(employeeId);
                    foreach (PenaltyEntity p in penalties)
                    {
                        p.PayRollId = payrollId;
                    }
                    repository.Complete();

                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;

        }

        public Result<string> addPayroll(DateTime date, long user, string file, long location)
        {
            Result<string> res = new Result<string>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<string> updatePayroll(long idPay, DateTime date, long user, string file)
        {
            Result<string> res = new Result<string>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<string> deletePayroll(long idPay)
        {
            Result<string> res = new Result<string>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<Payroll> selectPayroll(long idPay)
        {
            Result<Payroll> res = new Result<Payroll>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<List<Payroll>> selectAllPayroll(long idPay)
        {
            Result<List<Payroll>> res = new Result<List<Payroll>>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<List<Payroll>> selectPayroll(DateTime ini, DateTime end)
        {
            Result<List<Payroll>> res = new Result<List<Payroll>>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<string> addLocation(string name, double call_price)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = new LocationEntity()
                    { name = name, callPrice = call_price };
                    repository.Locations.Add(location);
                    repository.Complete();
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Location> getLocation(long id)
        {
            Result<Location> result = new Result<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);

                    if (location != null)
                    {
                        Location location_result = new Location()
                        {
                            Name = location.name,
                            CallPrice = (double)location.callPrice,
                            LastPayroll = (long)location.lastPayrollId,
                            CurrentPayroll = (long)location.currentPayrollId
                        };
                        result.Detail = location_result;
                    }
                    else
                    {
                        result.Status = inexistentLocation;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Location> location_Activate(long id)
        {
            Result<Location> result = new Result<Location>();
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
                        result.Status = location != null ? locationActive : inexistentLocation;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateLocation(long id, string name, double call_price)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentLocation;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateLocationLastPayroll(long id, long last_payroll)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentLocation;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateLocationCurrentPayroll(long id, long current_payroll)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentLocation;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> deleteLocation(long id)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null && location.active)
                    {
                        location.active = false;
                        //repository.Locations.Remove(location);
                        repository.Complete();
                    }
                    else
                    {
                        result.Status = inexistentLocation;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Location>> selectAllLocations()
        {
            Result<List<Location>> result = new Result<List<Location>>();
            result.Detail = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.GetAll();
                    foreach (var x in locations)
                    {
                        Location location = new Location()
                        { Id = x.id, Name = x.name, CallPrice = (double)x.callPrice }; //*
                        result.Detail.Add(location);
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Location>> selectAllActiveLocations()
        {
            Result<List<Location>> result = new Result<List<Location>>();
            result.Detail = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.getAllActiveLocations();
                    foreach (var x in locations)
                    {
                        Location location = new Location()
                        { Id = x.id, Name = x.name, CallPrice = (double)x.callPrice }; //*
                        result.Detail.Add(location);
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addRole(string name, long location, List<string> operations)//**
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = new RoleEntity()
                    { name = name, locationId = location };
                    repository.Roles.Add(role);
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
                result.Status = validate(e);
            }
            return result;
        }

        public Result<Role> getRole(long id)
        {
            Result<Role> result = new Result<Role>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);

                    if (role != null)
                    {
                        List<Tuple<string, string>> list = new List<Tuple<string, string>>();
                        foreach (var op in role.operations)
                        {
                            Tuple<string, string> tuple = new Tuple<string, string>(op.GroupId, op.Name.Split('/')[1]);
                            list.Add(tuple);
                        }
                        Role role_result = new Role(role.id, role.name, role.locationId, list);
                        result.Detail = role_result;
                    }
                    else
                    {
                        result.Status = inexistentRole;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateRole(long id, string name, long location, List<string> operations)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentRole;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> deleteRole(long id)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(id);
                    if (role != null)
                    {
                        repository.Roles.Remove(role);
                        repository.Complete();
                    }
                    else
                    {
                        result.Status = inexistentRole;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Role>> selectAllRoles()
        {
            Result<List<Role>> result = new Result<List<Role>>();
            result.Detail = new List<Role>();
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
                        result.Detail.Add(new Role(x.id, x.name, x.locationId, list));
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addOperation(string name, string description, string url, string group)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    OperationEntity operation = new OperationEntity()
                    { Name = name, Description = description, URL = url, GroupId = group };
                    repository.Operations.Add(operation);
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }
        /*
        public Result<string> updateOperationsToRole(long role_id, List<string> operations)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Get(role_id);
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
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }
        */

        private Result<Operation> getOperation(string id)
        {
            Result<Operation> result = new Result<Operation>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    OperationEntity operation = repository.Operations.Get(id);
                    if (operation != null)
                    {
                        Operation operation_result = new Operation(operation.Description, operation.Name, operation.GroupId);
                        result.Detail = operation_result;
                    }
                    else
                    {
                        result.Status = inexistentGroup;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        private Result<string> deleteOperation(string id)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentGroup;
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Operation>> selectAllOperation()
        {
            Result<List<Operation>> result = new Result<List<Operation>>();
            result.Detail = new List<Operation>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var os = repository.Operations.GetAll();
                    foreach (var x in os)
                        result.Detail.Add(new Operation(x.Description, x.URL, x.Name));
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<Operation>> selectAllOperationByGroup(String id_group)
        {
            Result<List<Operation>> result = new Result<List<Operation>>();
            result.Detail = new List<Operation>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var os = repository.Operations.selectOperationsByGroup(id_group);
                    foreach (var x in os)
                        result.Detail.Add(new Operation(x.Description, x.URL, x.Name));
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> addOperationGroup(string name, string description, string icon)
        {
            Result<string> result = new Result<string>();
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
                result.Status = validate(e);
            }
            return result;
        }

        private Result<OperationsGroup> getOperationGroup(string id)
        {
            Result<OperationsGroup> result = new Result<OperationsGroup>();
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
                            list.Add(new Operation(op.Description, op.Name, op.GroupId));
                        OperationsGroup group_result = new OperationsGroup(group.Description, group.Name, group.Icon, list);//**
                        result.Detail = group_result;
                    }
                    else
                    {
                        result.Status = inexistentGroup;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        private Result<string> deleteOperationGroup(string id)
        {
            Result<string> result = new Result<string>();
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
                        result.Status = inexistentGroup;
                    }
                }

            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<OperationsGroup>> selectAllOperationsGroup()
        {
            Result<List<OperationsGroup>> result = new Result<List<OperationsGroup>>();
            result.Detail = new List<OperationsGroup>();
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
                            list.Add(new Operation(op.Description, op.Name, op.GroupId));
                        result.Detail.Add(new OperationsGroup(x.Description, x.Name, x.Icon, list));
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = validate(e);
            }
            return result;
        }


        public Result<string> addUser(string name, string username, string password, long role, long location, string email)
        {
            Result<string> result = new Result<string>();
            try
            {

                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Users.Add(new UserEntity()
                    {
                        name = name,
                        email = email,
                        locationId = location,
                        password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()),
                        roleId = role,
                        userName = username.ToLower()
                    });

                    repository.Complete();
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateUser(long id, string name, string username, string password, long role, long location, string email)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        user.name = name;
                        user.userName = username;
                        user.password = password;
                        user.roleId = role;
                        user.locationId = location;
                        user.email = email;
                    }
                    else
                    {
                        result.Status = inexistentUser;
                    }
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<string> deleteUser(long id)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        repository.Users.Remove(user);
                    }
                    else
                    {
                        result.Status = inexistentUser;
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<User> selectUser(long id)
        {
            Result<User> result = new Result<User>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.Get(id);
                    if (user != null)
                    {
                        var newUser = new User()
                        {
                            Id = user.id,
                            Email = user.email,
                            Location = user.locationId,
                            Name = user.name,
                            Password = user.password,
                            Role = user.roleId,
                            Username = user.userName
                        };
                        result.Detail = newUser;
                    }
                    else
                    {
                        result.Status = inexistentUser;
                    }

                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<List<User>> selectAllUsers(long location)
        {
            Result<List<User>> result = new Result<List<User>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var users = repository.Users.GetAll();
                    foreach (UserEntity ue in users)
                    {
                        result.Detail.Add(new User()
                        {
                            Id = ue.id,
                            Email = ue.email,
                            Location = ue.locationId,
                            Name = ue.name,
                            Password = ue.password,
                            Role = ue.roleId,
                            Username = ue.userName
                        });
                    }
                }
            }
            catch (Exception e)
            {

                result.Status = validate(e);
            }

            return result;
        }

        public Result<User> login(string username, string password)
        {
            Result<User> res = new Result<User>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var user = repository.Users.login(username.ToLower(),password);
                    if (user != null)
                    {
                        res.Detail = new User() { Name = user.name, Id = user.id, Email = user.email, Location = user.locationId, Password = user.password, Role = user.roleId, Username = user.userName };
                    }
                    else
                    {
                        res.Status = inexistentUser;
                    }

                }
            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<List<Tuple<long, string>>> selectAllErrors()
        {
            Result<List<Tuple<long, string>>> res = new Result<List<Tuple<long, string>>>();
            res.Detail = new List<Tuple<long, string>>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var errors = repository.Errors.GetAll();
                    foreach (var error in errors)
                    {
                        res.Detail.Add(new Tuple<long, string>(error.id, error.message));
                    }
                }
            }
            catch (NpgsqlException e)
            {
                res.Status = validate(e);
            }
            return res;
        }

        public Result<string> addDebitType(string name, long location, long months = 0, double interestRate = 0)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    if (months == 0)
                    {
                        repository.DebitTypes.Add(new DebitTypeEntity()
                        {
                            name = name,
                            locationId = location,
                            interestRate = interestRate,
                            payment = false
                        });
                    }
                    else
                    {
                        repository.DebitTypes.Add(new DebitTypeEntity()
                        {
                            name = name,
                            locationId = location,
                            months = months,
                            interestRate = interestRate,
                            payment = true
                        });
                    }
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> updateDebitType(long id,string name, long months = 0, double interestRate = 0)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    if (months == 0)
                    {
                        type.name = name;
                        type.payment = false;
                    }
                    else
                    {
                        type.name = name;
                        type.months = months;
                        type.interestRate = interestRate;
                        type.payment = true;
                    }
                    repository.Complete();
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

        public Result<string> deleteDebitType(long id)
        {
            Result<string> result = new Result<string>();
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
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<DebitType>> selectFixedDebitTypes(long location)
        {
            Result<List<DebitType>> result = new Result<List<DebitType>>();
            try
            {
                result.Detail = new List<DebitType>();
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.DebitTypes.SelectByLocation(location);
                    foreach (var type in types)
                    {
                        if (!type.payment)
                        {
                            result.Detail.Add(new DebitType()
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
                result.Status = validate(e);
            }
            return result;
        }

        public Result<List<DebitType>> selectNonFixedDebitTypes(long location)
        {
            Result<List<DebitType>> result = new Result<List<DebitType>>();
            try
            {
                result.Detail = new List<DebitType>();
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var types = repository.DebitTypes.SelectByLocation(location);
                    foreach (var type in types)
                    {
                        if (type.payment)
                        {
                            result.Detail.Add(new DebitType()
                            {
                                id = type.id,
                                name = type.name,
                                location = type.locationId,
                                months = type.months,
                                interestRate = type.interestRate,
                                payment = true
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException e)
            {
                result.Status = validate(e);
            }
            return result;
        }

    }

}