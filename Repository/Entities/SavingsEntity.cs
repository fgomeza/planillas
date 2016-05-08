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
        [Column("employee"), Key, ForeignKey("fksaving")]
        public long employee { get; set; }

        public virtual EmployeeEntity fksaving { set; get; }


        public double amount { get; set; }

    }
}
