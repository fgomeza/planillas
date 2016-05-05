using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("administrators")]
    public class AdministratorEntity
    {

        [Column(Order =0),Key,ForeignKey("fkadministrator_user")]
        public long user_id { get; set; }
        public virtual UserEntity fkadministrator_user { get; set; }

        [Column(Order = 1), Key, ForeignKey("fkadministrator_location")]
        public long location { get; set; }
        public virtual LocationEntity fkadministrator_location { get; set; }
    }
}
