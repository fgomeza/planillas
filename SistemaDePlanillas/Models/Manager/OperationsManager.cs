using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class OperationsManager : IErrors
    {
        public void addOperation(string name, string description, string url, string group, bool isPayrollCalculationRelated)
        {
            Result<string> result = new Result<string>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    OperationEntity operation = new OperationEntity()
                    { Name = name, Description = description, GroupId = group , isPayrollCalculationRelated=isPayrollCalculationRelated};
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
                        result = new Operation(x.Name, x.Name.Split('/')[1], x.GroupId, x.Description,x.isPayrollCalculationRelated);
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
                        result.Add(new Operation(x.Name, x.Name.Split('/')[1], x.GroupId, x.Description,x.isPayrollCalculationRelated));
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
                        result.Add(new Operation(x.Name, x.Name.Split('/')[1], x.GroupId, x.Description,x.isPayrollCalculationRelated));
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