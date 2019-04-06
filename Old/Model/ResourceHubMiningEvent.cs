using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceHubMiningEvent
    {
        public int ResourceHubMiningEventId { get; set; }
        public int ResourceHubId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UtcDate { get; set; }
        public int QuantityMined { get; set; }
        public int ShipId { get; set; }

        [ForeignKey("ResourceHubId")]
        [InverseProperty("ResourceHubMiningEvents")]
        public virtual ResourceHub ResourceHub { get; set; }
        [ForeignKey("ShipId")]
        [InverseProperty("ResourceHubMiningEvents")]
        public virtual Ship Ship { get; set; }
    }
}