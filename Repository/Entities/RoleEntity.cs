using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("roles")]
    public class RoleEntity
    {
        [Key]
        [Column("id")]
        public long id { set; get; }

        [Index("UKROLE_NAME", 1, IsUnique = true)]
        public string name { set; get; }


        public bool active { set; get; }

        [ForeignKey("fkrole_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { set; get; }

        public virtual LocationEntity fkrole_location { set; get; }


    }
}