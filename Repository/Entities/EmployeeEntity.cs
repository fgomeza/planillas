using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Repositories.Classes;

namespace Repository.Entities
{
    [Table("employees")]
    public class EmployeeEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Column("name")]
        public string name { set; get; }

        [Column("id_card")]
        public string idCard { set; get; }

        [Column("cms")]
        public string cms { get; set; }


        [ForeignKey("fkemployee_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { get; set; }

        public virtual LocationEntity fkemployee_location { get; set; }

        [Column("active")]
        public bool active { get; set; }

        [Column("salary")]
        public double salary { get; set; }

        [Column("account")]
        public string account { get; set; }

        [Column("negativeamount")]
        public Nullable<double> negativeAmount { get; set; }

    }
}
