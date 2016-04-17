using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("savings")]
    public class SavingsEntity
    {
        [Key]
        [Column("employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { get; set; }

        public virtual EmployeeEntity employee { set; get; }

        [Column("amount")]
        public Nullable<double> amount { get; set; }

    }
}
