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
        public Debit addFixedDebit(long employee, string Detail, double amount, long type, long period)
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
                        period = period,
                        initialDate = DateTime.Today,
                        activeSince = DateTime.Today,
                        pastDays = 0,
                        active = true,
                    });
                    repository.Complete();
                    result = (Debit)toModel(debit);
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void updateFixedDebit(long idDebit, string Detail, double amount, long period)
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
                        debit.period = period;
                        repository.Complete();
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }

                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        debit.activeSince = DateTime.Today;
                        repository.Complete();
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        result = (Debit)toModel(debit);
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public List<Debit> selectActiveFixedDebits(long employee)
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
                            result.Add((Debit)toModel(debit));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public List<Debit> selectFixedDebits(long employee)
        {
            List<Debit> result = new List<Debit>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debits = repository.Debits.selectFixedDebitsByEmployee(employee);
                    foreach (var debit in debits)
                    {
                        result.Add((Debit)toModel(debit));
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public PaymentDebit addPaymentDebit(long employee, DateTime initialDate, string Detail, double total, long pays, long type, long period)
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
                        active = true,
                        period = period,
                        activeSince = DateTime.Today,
                        pastDays = 0
                    });
                    repository.Complete();
                    result = (PaymentDebit)toModel(debit);
                }

            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void updatePaymentDebit(long idDebit, double total, long remainingPays, long period)
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
                            throw validateException(App_LocalResoures.Errors.negativeAmount);

                        debit.totalAmount = total;
                        debit.remainingAmount = debit.totalAmount - paid;
                        debit.remainingPays = remainingPays;
                        debit.period = period;
                        repository.Complete();
                    }
                    else
                    {
                        throw validateException(debit != null ? App_LocalResoures.Errors.debitInactive : App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        private object toModel(DebitEntity debit) 
        {
            string type = debit.fkdebit_type.type;
            if (type == "P")
            {
                PaymentDebit result = new PaymentDebit();
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
                result.pastDays = debit.pastDays;
                return result;
            }
            else if (type == "A")
            {
                AmortizationDebit result = new AmortizationDebit();
                result.id = debit.id;
                result.initialDate = debit.initialDate;
                result.detail = debit.description;
                result.employee = debit.employeeId;
                result.total = debit.totalAmount;
                result.remainingAmount = debit.remainingAmount;
                result.paymentsMade = debit.paysMade;
                result.missingPayments = debit.remainingPays;
                result.interestRate = debit.fkdebit_type.interestRate;
                result.type = debit.debitTypeId;
                result.typeName = debit.fkdebit_type.name;
                result.pastDays = debit.pastDays;
                result.period = debit.period;
                return result;
            }
            else
            {
                return new Debit()
                {
                    id = debit.id,
                    employee = debit.employeeId,
                    amount = debit.totalAmount,
                    detail = debit.description,
                    type = debit.debitTypeId,
                    typeName = debit.fkdebit_type.name,
                    pastDays = debit.pastDays,
                    period = debit.period
                };
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
                        return (PaymentDebit)toModel(debit);
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            result.Add((PaymentDebit)toModel(debit));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        DebitId = idDebit,
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
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public AmortizationDebit addAmortizationDebit(long employee, string Detail, double total, long pays, long type, long period)
        {
            AmortizationDebit result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit = repository.Debits.Add(new DebitEntity()
                    {
                        initialDate = DateTime.Today,
                        description = Detail,
                        employeeId = employee,
                        totalAmount = total,
                        remainingAmount = total,
                        paysMade = 0,
                        remainingPays = pays,
                        debitTypeId = type,
                        active = true,
                        period = period,                      
                        activeSince = DateTime.Today
                    });
                    repository.Complete();
                    result = (AmortizationDebit)toModel(debit);
                }

            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        result = (AmortizationDebit)toModel(debit);
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentDebit);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            result.Add((AmortizationDebit)toModel(debit));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }



        public void addFixedDebitType(string name, long location, long period)
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
                        type = "F",
                        period=period
                    });

                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void addPaymentDebitType(string name, long location, long pays, double interestRate, long period)
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
                        type = "P",
                        period=period
                    });

                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void addAmortizationDebitType(string name, long location, long pays, double interestRate, long period)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    if (interestRate == 0)
                        throw validateException(App_LocalResoures.Errors.zeroInterestRate);
                    repository.DebitTypes.Add(new DebitTypeEntity()
                    {
                        name = name,
                        locationId = location,
                        pays = pays,
                        interestRate = interestRate,
                        type = "A",
                        period=period
                    });

                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void updateFixedDebitType(long id, string name, long period)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    type.name = name;
                    type.period = period;
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void updatePaymentDebitType(long id, string name, double interestRate, long pays, long period)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    type.name = name;
                    type.interestRate = interestRate;
                    type.pays = pays;
                    type.period = period;
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
        }

        public void updateAmortizationDebitType(long id, string name, double interestRate, long pays, long period)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    DebitTypeEntity type = repository.DebitTypes.Get(id);
                    type.name = name;
                    type.interestRate = interestRate;
                    type.pays = pays;
                    type.period = period;
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
            catch (Exception e)
            {
                throw validateException(e);
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
                                period = type.period
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                                period=type.period
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                                period=type.period
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void payDebit(long debitId, long payroll,long workedDays,DateTime initialDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var debit = repository.Debits.Get(debitId);
                    long payments = Math.Min(((workedDays + debit.pastDays) / debit.period), debit.remainingPays);
                    if (debit.fkdebit_type.type == "P")
                    {
                        double pay = (debit.remainingAmount / debit.remainingPays) * payments;
                        debit.remainingPays -= payments;
                        debit.remainingAmount = debit.remainingPays==0?0: debit.remainingAmount-pay;
                        debit.paysMade += payments;
                        for(int i = 1; i <= payments; i++)
                        {
                            repository.DebitPayments.Add(new DebitPaymentEntity()
                            {
                                DebitId = debit.id,
                                date = initialDate.AddDays((debit.period * i)-debit.pastDays),
                                payrollId = payroll,
                                Amount = pay + debit.totalAmount + debit.totalAmount * debit.fkdebit_type.interestRate,
                                InterestRate = debit.fkdebit_type.interestRate,
                                RemainingAmount = debit.remainingAmount
                            });
                        }
                        
                    }
                    else if (debit.fkdebit_type.type == "A")
                    {
                        double amortization = Models.Operations.PayrollGroup.calculateAmortization(debit.totalAmount, debit.remainingPays + debit.paysMade, debit.fkdebit_type.interestRate) * payments;
                        for (int i = 1; i <= payments; i++)
                        {
                            double pay = amortization - debit.remainingAmount * (debit.fkdebit_type.interestRate / 365) * debit.period;
                            debit.remainingPays -= 1;
                            debit.remainingAmount = debit.remainingPays==0?0 : debit.remainingAmount-pay;
                            debit.paysMade += 1;
                            repository.DebitPayments.Add(new DebitPaymentEntity()
                            {
                                DebitId = debit.id,
                                payrollId = payroll,
                                Amount = amortization,
                                InterestRate = debit.fkdebit_type.interestRate,
                                RemainingAmount = debit.remainingAmount
                            });
                        }
                           
                    }
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