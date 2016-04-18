using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("Penalties")]
    public class PenaltyEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("payroll")]
        [ForeignKey("fkpenalty_payroll")]
        public Nullable<long> PayRollId { get; set; }
        public virtual PayrollEntity fkpenalty_payroll { get; set; }

        [Column("employee")]
        [ForeignKey("fkpenalty_employee")]
        public long EmployeeId { get; set; }
        public virtual EmployeeEntity fkpenalty_employee { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("penalty_type")]
        [ForeignKey("fkpenalty_type")]
        public long PenaltyTypeId { get; set; }
        public virtual PenaltyTypeEntity fkpenalty_type { get; set; }

        [Column("amount")]
        public Nullable<long> Amount { get; set; }

        [Column("penalty_price")]
        public Nullable<double> PenaltyPrice { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("active")]
        public bool active { set; get; }
    }
}