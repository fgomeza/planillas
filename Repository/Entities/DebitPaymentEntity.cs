﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("debit_payments")]
    public class DebitPaymentEntity
    {

        [Column("DebitId", Order =1), Key ]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DebitId { set; get; }

        [Column("date", Order = 2), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime date { set; get; }

        [Column("payrollId"), ForeignKey("fkpayment_payroll")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long payrollId { get; set; }

        public virtual PayrollEntity fkpayment_payroll { set; get; }

        public double RemainingAmount { get; set; }


        public double InterestRate { get; set; }


        public double Amount { get; set; }

       

    }
}
