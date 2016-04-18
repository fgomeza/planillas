using Repository.Context;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    public class SavingRepository : Repository<SavingsEntity>
    {
        public SavingRepository(AppContext context) : base(context) { }

    }
}
