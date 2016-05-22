using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class PenaltiesManager : IErrors
    {
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
                    };
                    penalty = repository.Penalties.Add(penalty);
                    repository.Complete();
                    result = new Penalty()
                    {
                        id = penalty.Id,
                        amount = penalty.Amount,
                        date = penalty.Date,
                        detail = penalty.Description,
                        employee = penalty.EmployeeId,
                        type = penalty.PenaltyTypeId,
                        typeName = penalty.fkpenalty_type.name,
                        penaltyPrice =penalty.PenaltyPrice
                    };
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        penalty.payrollId = payroll;
                        penalty.Description = Detail;
                        penalty.PenaltyTypeId = penaltyTypeId;
                        penalty.Amount = amount;
                        penalty.PenaltyPrice = repository.PenaltyTypes.getPriceById(penaltyTypeId);
                        penalty.Date = date;
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                    var rows = repository.Complete();

                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        repository.Penalties.Remove(penalty);
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

        public Penalty selectPenalty(long idRecess)
        {
            Penalty result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    PenaltyEntity penalty = repository.Penalties.Get(idRecess);

                    if (penalty != null)
                    {
                        result = new Penalty()
                        {
                            id = penalty.Id,
                            amount = penalty.Amount,
                            date = penalty.Date,
                            detail = penalty.Description,
                            employee = penalty.EmployeeId,
                            type = penalty.PenaltyTypeId,
                            typeName = penalty.fkpenalty_type.name,
                            penaltyPrice = penalty.PenaltyPrice
                        };
                    }
                    else
                    {
                        throw validateException(penalty != null ? App_LocalResoures.Errors.penaltyInactive : App_LocalResoures.Errors.inexistentPenalty);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            typeName = p.fkpenalty_type.name,
                            penaltyPrice = p.PenaltyPrice
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
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
                        result.Add(p.Id, new PenaltyType() { id = p.Id, name = p.name, price = p.price });
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void assignPenaltiesToPayroll(long payrollId,long location, DateTime endDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Penalties.assignPayroll(payrollId,location, endDate);
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