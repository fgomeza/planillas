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
    public class DebitRepository: Repository<DebitEntity>
    {
        public DebitRepository(AppContext context) : base(context){}

        public IEnumerable<DebitEntity> selectDebitsByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId);
            return debits.ToList();
        }

        public IEnumerable<DebitEntity> selectFixDebitsByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId && e.payment == true);
            return debits.ToList();
        }

        public IEnumerable<DebitEntity> selectDebitsNonFixByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId && e.payment == false);
            return debits.ToList();
        }
    }
}
