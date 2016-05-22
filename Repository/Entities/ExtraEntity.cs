using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("extras")]
    public class ExtraEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [ForeignKey("fkextra_employee")]
        [Column("employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { set; get; }

        public virtual EmployeeEntity fkextra_employee { set; get; }


        public string description { set; get; }

        public DateTime date { set; get; }

        public long hours { set; get; }

        [Column("payroll"), ForeignKey("fkextra_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> payrollId { get; set; }

        public virtual PayrollEntity fkextra_payroll { set; get; }
    }
}
