using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class FactionOpinion
    {
        public int FactionOpinionId { get; set; }
        public int FactionId { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Opinion { get; set; }
        public int TargetFactionId { get; set; }

        [ForeignKey("FactionId")]
        [InverseProperty("FactionOpinionFactions")]
        public virtual Faction Faction { get; set; }
        [ForeignKey("TargetFactionId")]
        [InverseProperty("FactionOpinionTargetFactions")]
        public virtual Faction TargetFaction { get; set; }
    }
}