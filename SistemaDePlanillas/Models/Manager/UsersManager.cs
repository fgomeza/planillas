using DevOne.Security.Cryptography.BCrypt;
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
    public class UsersManager : IErrors
    {
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
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentEmployee);
                    }
                    repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                    var rows = repository.Complete();
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                        throw validateException(user != null ? App_LocalResoures.Errors.userInactive : App_LocalResoures.Errors.inexistentUser);
                    }
                }
            }
            catch (Exception e)
            {
                throw validateException(e);
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
                throw validateException(e);
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
                throw validateException(e);
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
                        throw validateException(App_LocalResoures.Errors.inexistentUser);
                    }
                }
            }
            catch (NpgsqlException e)
            {
                throw validateException(e);
            }
            return result;
        }
    }
}