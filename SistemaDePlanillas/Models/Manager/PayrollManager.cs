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
    public class PayrollManager : IErrors
    {
        public Payroll addPayroll(DateTime initialDate,DateTime endDate, double callPrice, long user, string json, long location)
        {
            Payroll result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var payroll = repository.PayRolls.Add(new PayrollEntity()
                    {
                        userId = user,
                        locationId = location,
                        endDate = endDate,
                        initialDate=initialDate,
                        callPrice = callPrice,
                        JSON = json
                    });
                    repository.Complete();
                    result = new Payroll()
                    {
                        id = payroll.id,
                        endDate = payroll.endDate,
                        initialDate=initialDate,
                        json = payroll.JSON,
                        user = payroll.userId
                    };
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

        public void updatePayroll(long idPay, DateTime date, long user, string file)
        {
            try
            {

            }
            catch (NpgsqlException e)
            {
                throw validateException(e);
            }
        }

        public void deletePayroll(long idPay)
        {
            try
            {

            }
            catch (NpgsqlException e)
            {
                throw validateException(e);
            }
        }

        public Payroll selectPayroll(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var payroll = repository.PayRolls.Get(id);
                    if (payroll == null)
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentPayroll);
                    }
                    return new Payroll()
                    {
                        id = payroll.id,
                        endDate = payroll.endDate,
                        initialDate=payroll.initialDate,
                        user = payroll.userId,
                        json = payroll.JSON
                    };
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
                return null;
            }
        }

        public List<Payroll> selectAllPayroll(long idPay)
        {
            List<Payroll> result = new List<Payroll>();
            try
            {

            }
            catch (NpgsqlException e)
            {
                throw validateException(e);
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
                throw validateException(e);
            }
            return result;
        }
    }
}