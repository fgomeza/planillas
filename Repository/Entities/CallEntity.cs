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
        [Column("employee", Order =0),Key, ForeignKey("fkcall_employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { get; set; }

        public virtual EmployeeEntity fkcall_employee { set; get; }

        [Column("date", Order =1),Key]
        public System.DateTime date { get; set; }

        [Column("calls")]
        public long calls { get; set; }

        [Column("time")]
        public Nullable<System.TimeSpan> time { get; set; }

        [Column("payroll"),ForeignKey("fkcall_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> payrollId { get; set; }

        public virtual PayrollEntity fkcall_payroll { set; get; }

    }
}
