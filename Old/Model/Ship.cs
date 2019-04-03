using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Ship
    {
        public Ship()
        {
            ResourceHubMiningEvents = new HashSet<ResourceHubMiningEvent>();
            ShipCrewSlots = new HashSet<ShipCrewSlot>();
        }

        public int ShipId { get; set; }
        public int ShipTypeId { get; set; }
        public int? PlayerId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? FactionId { get; set; }
        [Required]
        public bool? IsAlive { get; set; }
        public int StarSystemId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }

        [ForeignKey("PlayerId")]
        [InverseProperty("Ships")]
        public virtual Player Player { get; set; }
        [ForeignKey("ShipTypeId")]
        [InverseProperty("Ships")]
        public virtual ShipType ShipType { get; set; }
        [ForeignKey("StarSystemId")]
        [InverseProperty("Ships")]
        public virtual StarSystem StarSystem { get; set; }
        [InverseProperty("Ship")]
        public virtual ICollection<ResourceHubMiningEvent> ResourceHubMiningEvents { get; set; }
        [InverseProperty("Ship")]
        public virtual ICollection<ShipCrewSlot> ShipCrewSlots { get; set; }
    }
}