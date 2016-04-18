﻿using System;
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

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public Nullable<double> Price { get; set; }

        [Column("location"), ForeignKey("fkpenaltytype_location")]
        public long location { get; set; }
        public virtual LocationEntity fkpenaltytype_location { set; get; }
    }
}