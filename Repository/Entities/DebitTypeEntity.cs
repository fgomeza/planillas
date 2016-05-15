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


        public string name { set; get; }


        public long pays { set; get; }


        public double interestRate { set; get; }

        [ForeignKey("debittype_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        public virtual LocationEntity debittype_location { set; get; }

        public string type { set; get; }
    }
}
