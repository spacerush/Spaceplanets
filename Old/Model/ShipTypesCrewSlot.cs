using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipTypesCrewSlot
    {
        public int ShipTypesCrewSlotId { get; set; }
        public int CrewSlotId { get; set; }
        public int ShipTypeId { get; set; }
        public int Quantity { get; set; }
        public int? SlotDependencyId { get; set; }

        [ForeignKey("CrewSlotId")]
        [InverseProperty("ShipTypesCrewSlotCrewSlots")]
        public virtual CrewSlot CrewSlot { get; set; }
        [ForeignKey("ShipTypeId")]
        [InverseProperty("ShipTypesCrewSlots")]
        public virtual ShipType ShipType { get; set; }
        [ForeignKey("SlotDependencyId")]
        [InverseProperty("ShipTypesCrewSlotSlotDependencies")]
        public virtual CrewSlot SlotDependency { get; set; }
    }
}