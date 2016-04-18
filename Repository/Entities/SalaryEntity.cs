using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{   [Table("salary")]
    public class SalaryEntity
    {

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("fksalary_payroll")]
        [Column("payroll", Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long payrollId { get; set; }

        public virtual PayrollEntity fksalary_payroll { set; get; }


        [ForeignKey("fksalary_employee")]
        [Column("employee", Order =2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { get; set; }

        public virtual EmployeeEntity fksalary_employee { set; get; }

        [Column("net_salary")]
        public Nullable<double> netSalary { get; set; }

        [Column("salary")]
        public Nullable<double> salary { get; set; }

    }
}
