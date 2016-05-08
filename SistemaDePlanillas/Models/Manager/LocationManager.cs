using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class LocationManager : IErrors
    {
        public Location addLocation(string name, double call_price, long administrator)
        {
            Location result = null;
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = new LocationEntity()
                    { name = name, callPrice = call_price, active = true , isPendingToApprove= false};
                    location = repository.Locations.Add(location);
                    repository.Complete();
                    updateAdministrator(location.id, administrator);
                    result = new Location();
                    result.Name = location.name;
                    result.CallPrice = (long)location.callPrice;
                    result.LastPayroll = location.lastPayrollId;
                    result.CurrentPayroll = location.currentPayrollId;
                    result.Active = location.active;
                    result.isPendingToApprove = location.isPendingToApprove;
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public Location getLocation(long id)
        {
            Location result = new Location();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);

                    if (location != null && location.active)
                    {
                        result.Name = location.name;
                        result.CallPrice = location.callPrice;
                        result.LastPayroll = location.lastPayrollId;
                        result.CurrentPayroll = location.currentPayrollId;
                        result.Active = location.active;
                        result.isPendingToApprove = location.isPendingToApprove;
                    }
                    else
                    {
                        validateException(location != null ? App_LocalResoures.Errors.locationInactive : App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public void activateLocation(long id)
        {
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
                        validateException(location != null ? App_LocalResoures.Errors.locationInactive : App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateAdministrator(long location, long administrator)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    AdministratorEntity admin = repository.Administrators.Get(location);
                    if (admin != null)
                    {
                        admin.user_id = administrator;
                    }
                    else
                    {
                        repository.Administrators.Add(new AdministratorEntity()
                        {
                            location = location,
                            user_id = administrator
                        });
                    }
                    repository.Complete();
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocation(long id, string name, double call_price)
        {
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
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocationLastPayroll(long id, long last_payroll)
        {
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
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void updateLocationCurrentPayroll(long id, Nullable<long> current_payroll)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null)
                    {
                        if (location.currentPayrollId != null)
                        {
                            var current = repository.PayRolls.Get((long)location.currentPayrollId);
                            repository.PayRolls.Remove(current);

                        }
                        location.currentPayrollId = current_payroll;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void setPendingToApprove(long id, bool approve)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null && location.active)
                    {
                        location.isPendingToApprove = approve;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public void deleteLocation(long id)
        {
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    LocationEntity location = repository.Locations.Get(id);
                    if (location != null && location.active)
                    {
                        location.active = false;
                        repository.Complete();
                    }
                    else
                    {
                        validateException(App_LocalResoures.Errors.inexistentLocation);
                    }
                }

            }
            catch (Exception e)
            {
                validateException(e);
            }
        }

        public List<Location> selectAllLocations()
        {
            List<Location> result = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.GetAll();
                    foreach (var x in locations)
                    {
                        result.Add(new Location()
                        {
                            Id = x.id,
                            Name = x.name,
                            CallPrice = (double)x.callPrice
                        });
                    }
                }
            }
            catch (Exception e)
            {
                validateException(e);
            }
            return result;
        }

        public List<Location> selectAllActiveLocations()
        {
            List<Location> result = new List<Location>();
            try
            {
                using (var repository = new MainRepository(new AppContext("PostgresConnection")))
                {
                    var locations = repository.Locations.getAllActiveLocations();
                    foreach (var x in locations)
                    {
                        result.Add(new Location()
                        {
                            Id = x.id,
                            Name = x.name,
                            CallPrice = (double)x.callPrice
                        });
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