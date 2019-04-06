using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class CharacterSlottedImplant
    {
        public int CharacterSlottedImplantId { get; set; }
        public int CharacterId { get; set; }
        public int ImplantSlotId { get; set; }
        public int ImplantId { get; set; }

        [ForeignKey("CharacterId")]
        [InverseProperty("CharacterSlottedImplants")]
        public virtual Character Character { get; set; }
        [ForeignKey("ImplantId")]
        [InverseProperty("CharacterSlottedImplants")]
        public virtual Implant Implant { get; set; }
        [ForeignKey("ImplantSlotId")]
        [InverseProperty("CharacterSlottedImplants")]
        public virtual ImplantSlot ImplantSlot { get; set; }
    }
}