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


        public string name { set; get; }

        [Index("UKEMPLOYEE_IDCARD", 1, IsUnique = true)]
        public string idCard { set; get; }

        [Index("UKEMPLOYEE_CMS", 2, IsUnique = true)]
        public string cms { get; set; }

        public bool iscms { get; set; }

        [ForeignKey("fkemployee_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { get; set; }

        public virtual LocationEntity fkemployee_location { get; set; }


        public bool active { get; set; }


        public double salary { get; set; }


        public string account { get; set; }

        public double negativeAmount { get; set; }

    }
}
