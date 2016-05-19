using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("RoleOperation")]
    public class RoleOperationEntity
    {
        [Column("operation", Order = 0), Key, ForeignKey("fkRoleOperation_Operation")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string operation { get; set; }

        public virtual OperationEntity fkRoleOperation_Operation { set; get; }


        [Column("role", Order = 1), Key, ForeignKey("fkRoleOperation_Role")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long role { get; set; }

        public virtual RoleEntity fkRoleOperation_Role { set; get; }
    }
}
