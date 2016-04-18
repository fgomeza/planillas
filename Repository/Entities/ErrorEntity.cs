using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    [Table("errors")]
    public class ErrorEntity
    {
        
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }
        [Key]
        [Column("message")]
        public string message { get; set; }
    }
}
