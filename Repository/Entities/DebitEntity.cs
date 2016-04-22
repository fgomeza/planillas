using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("debits")]
    public class DebitEntity
    {
        [Column("id"),Key]
        public long id { set; get; }

        [Column("initial_date")]
        public DateTime initialDate { set; get; }

        [Column("description")]
        public string description { set; get; }

        [Column("employee"), ForeignKey("fkdebit_employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { set; get; }

        public virtual EmployeeEntity fkdebit_employee { set; get; }

        [Column("total_amount")]
        public double totalAmount { set; get; }

        [Column("remaining_amount")]
        public double remainingAmount { set; get; }

        [Column("remaining_months")]
        public Nullable<long> remainingMonths { set; get; }
        

        [Column("paid_months")]
        public Nullable<long> paidMonths { set; get; }

        [Column("type"), ForeignKey("fkdebit_type")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long debitTypeId { set; get; }

        public virtual DebitTypeEntity fkdebit_type { set; get; }

        [Column("payment")]
        public bool payment { set; get; }

        [Column("active")]
        public bool active { set; get; }
    }
}
