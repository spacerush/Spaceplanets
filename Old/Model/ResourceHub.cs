using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceHub
    {
        public ResourceHub()
        {
            ResourceHubMiningEvents = new HashSet<ResourceHubMiningEvent>();
        }

        public int ResourceHubId { get; set; }
        public int SpaceObjectId { get; set; }
        public int ResourceId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? UnitsMineablePerMinute { get; set; }
        public int? UnitsMineablePerHour { get; set; }
        public int? UnitsMineablePerDay { get; set; }
        public int? UnitsMineablePerWeek { get; set; }
        public int StartingMineBalance { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ResourceHubs")]
        public virtual Resource Resource { get; set; }
        [ForeignKey("SpaceObjectId")]
        [InverseProperty("ResourceHubs")]
        public virtual SpaceObject SpaceObject { get; set; }
        [InverseProperty("ResourceHub")]
        public virtual ICollection<ResourceHubMiningEvent> ResourceHubMiningEvents { get; set; }
    }
}