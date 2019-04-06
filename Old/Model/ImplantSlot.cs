using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ImplantSlot
    {
        public ImplantSlot()
        {
            CharacterSlottedImplants = new HashSet<CharacterSlottedImplant>();
            ImplantBaseItems = new HashSet<ImplantBaseItem>();
        }

        public int ImplantSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ImplantSlot")]
        public virtual ICollection<CharacterSlottedImplant> CharacterSlottedImplants { get; set; }
        [InverseProperty("ImplantSlot")]
        public virtual ICollection<ImplantBaseItem> ImplantBaseItems { get; set; }
    }
}