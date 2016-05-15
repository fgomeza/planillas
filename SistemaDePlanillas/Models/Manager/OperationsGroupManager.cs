using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class OperationsGroupManager : IErrors
    {
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
                throw validateException(e);
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
                            list.Add(new Operation(op.Name, op.Name.Split('/')[1], op.GroupId, op.Description, op.isPayrollCalculationRelated));
                        result = new OperationsGroup(group.Description, group.Name, group.Icon, list);
                    }
                    else
                    {
                        throw validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }

            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentGroup);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                            list.Add(new Operation(op.Name, op.Name.Split('/')[1], op.GroupId, op.Description,op.isPayrollCalculationRelated));
                        result.Add(new OperationsGroup(x.Description, x.Name, x.Icon, list));
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
            }
            return result;
        }

    }
}