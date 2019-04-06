using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipCrewSlot
    {
        public int ShipCrewSlotId { get; set; }
        public int CrewSlotId { get; set; }
        public int ShipId { get; set; }
        public int CharacterId { get; set; }

        [ForeignKey("CharacterId")]
        [InverseProperty("ShipCrewSlots")]
        public virtual Character Character { get; set; }
        [ForeignKey("CrewSlotId")]
        [InverseProperty("ShipCrewSlots")]
        public virtual CrewSlot CrewSlot { get; set; }
        [ForeignKey("ShipId")]
        [InverseProperty("ShipCrewSlots")]
        public virtual Ship Ship { get; set; }
    }
}