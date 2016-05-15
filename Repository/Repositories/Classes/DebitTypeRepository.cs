using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class DebitTypeRepository :Repository<DebitTypeEntity>
    {
        public DebitTypeRepository(AppContext context) : base(context){ }

        public IEnumerable<DebitTypeEntity> SelectByLocation(long location)
        {
            return _context.DebitTypes.Where((d) => d.locationId == location).ToList();
        }
    }
}
