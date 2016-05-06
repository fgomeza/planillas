﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("debits")]
    public class DebitEntity
    {
        [Column("id"), Key]
        public long id { set; get; }


        public DateTime initialDate { set; get; }


        public string description { set; get; }

        [Column("employee"), ForeignKey("fkdebit_employee")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long employeeId { set; get; }

        public virtual EmployeeEntity fkdebit_employee { set; get; }


        public double totalAmount { set; get; }


        public double remainingAmount { set; get; }


        public long remainingPays { set; get; }

        public long paysMade { set; get; }

        [Column("type"), ForeignKey("fkdebit_type")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long debitTypeId { set; get; }

        public virtual DebitTypeEntity fkdebit_type { set; get; }

        public bool active { set; get; }
    }
}
