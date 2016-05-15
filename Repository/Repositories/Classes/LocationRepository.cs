using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Context;

namespace Repository.Repositories.Classes
{
    public class LocationRepository: Repository<LocationEntity>
    {
        public LocationRepository(AppContext context) : base(context){}

        public LocationEntity  lastLocation(string name)
        {
            return _context.Locations.Last(l => l.name == name);
        }

        public IEnumerable<LocationEntity> getAllActiveLocations()
        {
            var locations = _context.Locations.Where((e) => e.active == true);
            return locations.ToList();
        }
    }
}
