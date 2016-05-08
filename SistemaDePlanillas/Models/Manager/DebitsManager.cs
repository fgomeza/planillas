using Npgsql;
using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class DebitsManager : IErrors
    {
        public Debit addFixedDebit(long employee, string Detail, double amount, long type)
        {
            Debit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit = repository.Debits.Add(new DebitEntity()
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
                        validateException(debit != null ? App_LocalResoures.Errors.debitInactive : App_LocalResoures.Errors.inexistentDebit);
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
                                paymentsMade = (long)debit.paysMade,
                                missingPayments = (long)debit.remainingPays,
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
                                paymentsMade = (long)debit.paysMade,
                                missingPayments = (long)debit.remainingPays,
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
                    pays = pays,
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
                    if (type.type == "F")
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
                    if (type.type == "P")
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