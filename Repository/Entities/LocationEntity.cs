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

        [Index("UKLOCATION_NAME", 1, IsUnique = true)]
        public string name { set; get; }


        public double callPrice { set; get; }


        public bool active { set; get; }

        [ForeignKey("fklocation_lastpayroll")]
        [Column("last_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> lastPayrollId { set; get; }

        public virtual PayrollEntity fklocation_lastpayroll { set; get; }

        [ForeignKey("fklocation_currentpayroll")]
        [Column("current_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> currentPayrollId { set; get; }

        public virtual PayrollEntity fklocation_currentpayroll { set; get; }


    }
}

