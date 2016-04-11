using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("Administrators")]
    public class AdministratorEntity
    {
        [Key]
        [Column("user_id")]
        [ForeignKey("fkadministrator_user")]
        public long Id { get; set; }
        public virtual UserEntity fkadministrator_user { get; set; }

        [Column("location")]
        [ForeignKey("fkadministrator_location")]
        public long LocationId { get; set; }
        public virtual LocationEntity fkadministrator_location { get; set; }
    }
}
