using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class CharacterImplantInventoryItem
    {
        [Key]
        public int CharacterInventoryItemId { get; set; }
        public int CharacterId { get; set; }
        public int ImplantId { get; set; }

        [ForeignKey("CharacterId")]
        [InverseProperty("CharacterImplantInventoryItems")]
        public virtual Character Character { get; set; }
        [ForeignKey("ImplantId")]
        [InverseProperty("CharacterImplantInventoryItem")]
        public virtual Implant Implant { get; set; }
    }
}