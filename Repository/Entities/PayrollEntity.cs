using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("payrolls")]
    public class PayrollEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Index("UKpayroll_ENDATE", IsUnique = true)]
        public DateTime endDate { set; get; }

        [Index("UKpayroll_INITIALDATE", IsUnique = true)]
        public DateTime initialDate { set; get; }

        [ForeignKey("fkpayroll_user")]
        [Column("user_id")]
        public long userId { set; get; }
        public virtual UserEntity fkpayroll_user { get; set; }


        public Nullable<double> callPrice { set; get; }

        [ForeignKey("fkpayroll_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        public virtual LocationEntity fkpayroll_location { set; get; }

        public string JSON { set; get; }
    }
}
