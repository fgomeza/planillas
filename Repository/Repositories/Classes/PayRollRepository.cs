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
    public class PayRollRepository:Repository<PayrollEntity>
    {
        public PayRollRepository(AppContext context) : base(context){}
    }
}
