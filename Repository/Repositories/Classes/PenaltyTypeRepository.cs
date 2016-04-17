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
    public class PenaltyTypeRepository : Repository<PenaltyTypeEntity>
    {
        public PenaltyTypeRepository(AppContext context) : base(context)
        {
        }

        public Nullable<double> getPriceById(long id)
        {
            var penalty = _context.PenaltyTypes.Find(id);
            return penalty.Price;
        }

        public IEnumerable<PenaltyTypeEntity> getAllbyLocation(long location)
        {
            return _context.PenaltyTypes.Where(p => p.location == location).ToList();
        }

        public string getTypeName(long id)
        {
            return _context.PenaltyTypes.First(t => t.Id == id).Name;
        }
    }
}
