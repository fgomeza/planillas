using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("vacations")]
    public class VacationEntity
    {
        [Column("employee", Order = 0), Key, ForeignKey("fkvacation_employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { get; set; }

        public virtual EmployeeEntity fkvacation_employee { set; get; }

        [Column("date", Order = 1), Key]
        public DateTime date { get; set; }

        [Column("payroll"), ForeignKey("fkvacation_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Nullable<long> payrollId { get; set; }

        public virtual PayrollEntity fkvacation_payroll { set; get; }

        public double vacationsPrice { set; get; }
    }
}
