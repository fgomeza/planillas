using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Classes
{
    [Table("calls")]
    public class CallEntity
    {
        [Key]
        [ForeignKey("fkcall_employee")]
        [Column("employee", Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { get; set; }

        public virtual EmployeeEntity fkcall_employee { set; get; }

        [Key]
        [Column("date", Order =2)]
        public System.DateTime date { get; set; }

        [Column("calls")]
        public long calls { get; set; }

        [Column("time")]
        public Nullable<System.TimeSpan> time { get; set; }

        [ForeignKey("fkcall_payroll")]
        [Column("payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> payrollId { get; set; }

        public virtual PayrollEntity fkcall_payroll { set; get; }

    }
}
