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


        public IEnumerable<DebitEntity> selectFixedDebitsByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId && e.active && e.remainingAmount>0 && e.fkdebit_type.type == "F");
            return debits.ToList();
        }

        public IEnumerable<DebitEntity> selectPaymentDebitsByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId && e.active && e.remainingAmount > 0 && e.fkdebit_type.type == "P");
            return debits.ToList();
        }

        public IEnumerable<DebitEntity> selectAmortizationDebitsByEmployee(long employeeId)
        {
            var debits = _context.Debits.Where(e => e.employeeId == employeeId && e.active && e.remainingAmount > 0 && e.fkdebit_type.type == "A");
            return debits.ToList();
        }
    }
}
