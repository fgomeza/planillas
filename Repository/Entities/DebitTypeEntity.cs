using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("debit_types")]
    public class DebitTypeEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Column("name")]
        public string name { set; get; }

        [Column("months")]
        public Nullable<long> months { set; get; }

        [Column("interest_rate")]
        public Nullable<double> interestRate { set; get; }

        [ForeignKey("debittype_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        public virtual LocationEntity debittype_location { set; get; }

        [Column("payment")]
        public bool payment { set; get; }
    }
}
