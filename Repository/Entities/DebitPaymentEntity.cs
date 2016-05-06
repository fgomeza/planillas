using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("DEBIT_PAYMENTS")]
    public class DebitPaymentEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [ForeignKey("fkdebitpayment")]
        [Column("debit")]
        [Index("UKPAYMENT_DEBIT", 1, IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long debitId { set; get; }

        [Index("UKPAYMENT_DATE", 2, IsUnique = true)]
        public DateTime Date { get; set; }


        public double RemainingAmount { get; set; }


        public double InterestRare { get; set; }


        public double Amount { get; set; }

    }
}
