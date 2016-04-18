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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long debitId { set; get; }

        [Column("DATE")]
        public DateTime Date { get; set; }
        
        [Column("REMAINING_AMOUNT")]
        public Nullable<double> RemainingAmount { get; set; }

        [Column("INTEREST_RATE")]
        public Nullable<double> InterestRare { get; set; }

        [Column("AMMOUNT")]
        public Nullable<double> Amount { get; set; }


    }
}
