using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("operations")]
    public class OperationEntity
    {
        [Key]
        [Column("name")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { set; get; }


        public string Description { set; get; }

        public bool isPayrollCalculationRelated { set; get; }

        [ForeignKey("fkoperation_group")]
        [Column("group_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GroupId { set; get; }

        public virtual GroupEntity fkoperation_group { set; get; }

        public virtual ICollection<RoleEntity> Roles { set; get; }

    }
}
