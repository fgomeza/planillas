using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Repository.Entities
{
    [Table("groups")]
    public class GroupEntity
    {
        [Key]
        [Column("name")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("icon")]
        public string Icon { get; set; }

        //public virtual ICollection<OperationEntity> Operations { set; get; }
    }
}