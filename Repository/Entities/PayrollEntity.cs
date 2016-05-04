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

        [Column("end_date")]
        public DateTime endDate { set; get; }

        [ForeignKey("fkpayroll_user")]
        [Column("user_id")]
        public long userId { set; get; }
        public virtual UserEntity fkpayroll_user { get; set; }

        [Column("call_price")]
        public Nullable<double> callPrice { set; get; }

        [ForeignKey("fkpayroll_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        [Column("json")]
        public string JSON { set; get; }

        public virtual LocationEntity fkpayroll_location { set; get; }
    }
}
