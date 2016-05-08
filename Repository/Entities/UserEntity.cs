using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        public long id { get; set; }


        public string name { get; set; }

        [Index("UKUSER_EMAIL", 1, IsUnique = true)]
        public string email { get; set; }

        [Index("UKUSER_USERNAME", 2, IsUnique = true)]
        public string userName { get; set; }


        public string password { get; set; }


        public bool active { set; get; }

        [ForeignKey("fkuser_role")]
        [Column("role")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long roleId { get; set; }

        public virtual RoleEntity fkuser_role { set; get; }

        [ForeignKey("fkuser_location")]
        [Column("location")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long locationId { get; set; }

        public virtual LocationEntity fkuser_location { set; get; }

    }
}
