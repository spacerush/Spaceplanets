using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Faction
    {
        public Faction()
        {
            FactionOpinionFactions = new HashSet<FactionOpinion>();
            FactionOpinionTargetFactions = new HashSet<FactionOpinion>();
        }

        public int FactionId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Faction")]
        public virtual ICollection<FactionOpinion> FactionOpinionFactions { get; set; }
        [InverseProperty("TargetFaction")]
        public virtual ICollection<FactionOpinion> FactionOpinionTargetFactions { get; set; }
    }
}