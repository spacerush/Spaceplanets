using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipType
    {
        public ShipType()
        {
            ShipTypesCrewSlots = new HashSet<ShipTypesCrewSlot>();
            ShipTypesResources = new HashSet<ShipTypesResource>();
            Ships = new HashSet<Ship>();
            ShipyardShipTypes = new HashSet<ShipyardShipType>();
        }

        public int ShipTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ShipType")]
        public virtual ICollection<ShipTypesCrewSlot> ShipTypesCrewSlots { get; set; }
        [InverseProperty("ShipType")]
        public virtual ICollection<ShipTypesResource> ShipTypesResources { get; set; }
        [InverseProperty("ShipType")]
        public virtual ICollection<Ship> Ships { get; set; }
        [InverseProperty("ShipType")]
        public virtual ICollection<ShipyardShipType> ShipyardShipTypes { get; set; }
    }
}