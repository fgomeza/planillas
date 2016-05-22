using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class ExtrasManager : IErrors
    {
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
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentExtra);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public List<Extra> selectExtras(long employee, DateTime date)
        {
            List<Extra> result = new List<Extra>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var extras = repository.Extras.selectExtrasByEmployee(employee,date);
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
                throw validateException(e);
            }
            return result;
        }

        public void assignExtrasToPayroll(long payrollId, long location,DateTime endDate)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    repository.Extras.assignPayroll(payrollId, location,endDate);
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