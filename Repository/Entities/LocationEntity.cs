using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("locations")]
    public class LocationEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Column("name")]
        public string name { set; get; }

        [Column("call_price")]
        public Nullable<double> callPrice { set; get; }

        [ForeignKey("fklocation_lastpayroll")]
        [Column("last_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public System.Nullable<long> lastPayrollId { set; get; }

        public virtual PayrollEntity fklocation_lastpayroll { set; get; }

        [ForeignKey("fklocation_currentpayroll")]
        [Column("current_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public System.Nullable<long> currentPayrollId {set;get;}

        public virtual PayrollEntity fklocation_currentpayroll { set; get; }

    }
}

