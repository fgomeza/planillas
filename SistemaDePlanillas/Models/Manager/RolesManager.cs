using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class RolesManager :IErrors
    {
        public Role addRole(string name, long location, List<string> operations)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    RoleEntity role = repository.Roles.Add(new RoleEntity()
                    { name = name, locationId = location, active = true });

                    foreach (var op in operations)
                    {
                        OperationEntity operation = repository.Operations.Get(op);
                        if (operation != null)
                        {
                            role.operations.Add(operation);
                        }
                    }
                    repository.Complete();
                    List<Tuple<string, string, bool>> list = new List<Tuple<string, string, bool>>();
                    foreach (var op in role.operations)
                    {
                        Tuple<string, string, bool> tuple = new Tuple<string, string, bool>(op.GroupId, op.Name.Split('/')[1], op.isPayrollCalculationRelated);
                        list.Add(tuple);
                    }
                    return new Role(role.id, role.name, role.locationId, role.active, list);
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return null;
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
                        List<Tuple<string, string, bool>> list = new List<Tuple<string, string, bool>>();
                        foreach (var op in role.operations)
                        {
                            Tuple<string, string,bool> tuple = new Tuple<string, string,bool>(op.GroupId, op.Name.Split('/')[1],op.isPayrollCalculationRelated);
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
                        //var Users = selectAllActiveUsers(role.locationId);
                        List<User> userResult = new List<User>();

                        var users = repository.Users.GetAll();
                        foreach (UserEntity ue in users)
                        {
                            if (ue.active == true)
                            {
                                userResult.Add(new User()
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
                            
                        if (userResult.TrueForAll(u => u.Role != role.id))
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
                        List<Tuple<string, string, bool>> list = new List<Tuple<string, string, bool>>();
                        foreach (var op in x.operations)
                        {
                            Tuple<string, string, bool> tuple = new Tuple<string, string, bool>(op.GroupId, op.Name.Split('/')[1], op.isPayrollCalculationRelated);
                            list.Add(tuple);
                        }
                        result.Add(new Role(x.id, x.name, x.locationId, x.active, list));
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
                            List<Tuple<string, string, bool>> list = new List<Tuple<string, string, bool>>();
                            foreach (var op in x.operations)
                            {
                                Tuple<string, string, bool> tuple = new Tuple<string, string, bool>(op.GroupId, op.Name.Split('/')[1], op.isPayrollCalculationRelated);
                                list.Add(tuple);
                            }
                            result.Add(new Role(x.id, x.name, x.locationId, x.active, list));
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
    }
}