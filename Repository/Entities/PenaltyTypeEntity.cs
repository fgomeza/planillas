using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Repository.Entities
{
    [Table("penalty_types")]
    public class PenaltyTypeEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }


        public string name { get; set; }


        public double price { get; set; }

        [Column("location"), ForeignKey("fkpenaltytype_location")]
        public long location { get; set; }
        public virtual LocationEntity fkpenaltytype_location { set; get; }
    }
}