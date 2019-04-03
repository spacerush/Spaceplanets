using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class CrewSlot
    {
        public CrewSlot()
        {
            ShipCrewSlots = new HashSet<ShipCrewSlot>();
            ShipTypesCrewSlotCrewSlots = new HashSet<ShipTypesCrewSlot>();
            ShipTypesCrewSlotSlotDependencies = new HashSet<ShipTypesCrewSlot>();
        }

        public int CrewSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("CrewSlot")]
        public virtual ICollection<ShipCrewSlot> ShipCrewSlots { get; set; }
        [InverseProperty("CrewSlot")]
        public virtual ICollection<ShipTypesCrewSlot> ShipTypesCrewSlotCrewSlots { get; set; }
        [InverseProperty("SlotDependency")]
        public virtual ICollection<ShipTypesCrewSlot> ShipTypesCrewSlotSlotDependencies { get; set; }
    }
}