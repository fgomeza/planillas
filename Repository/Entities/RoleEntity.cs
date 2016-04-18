using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{   [Table("roles")]
    public class RoleEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Column("name")]
        public string name { set; get; }

        [Column("active")]
        public Nullable<bool> active { set; get; }

        [ForeignKey("fkrole_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        public virtual LocationEntity fkrole_location { set; get; }


        public virtual ICollection<OperationEntity> operations { set; get; }
    }
}