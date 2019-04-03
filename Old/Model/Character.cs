using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Character
    {
        public Character()
        {
            CharacterImplantInventoryItems = new HashSet<CharacterImplantInventoryItem>();
            CharacterSlottedImplants = new HashSet<CharacterSlottedImplant>();
            ShipCrewSlots = new HashSet<ShipCrewSlot>();
        }

        public int CharacterId { get; set; }
        public int ProfessionId { get; set; }
        public int GenderId { get; set; }
        public int PlayerId { get; set; }
        [StringLength(50)]
        public string CharacterName { get; set; }

        [ForeignKey("GenderId")]
        [InverseProperty("Characters")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("PlayerId")]
        [InverseProperty("Characters")]
        public virtual Player Player { get; set; }
        [ForeignKey("ProfessionId")]
        [InverseProperty("Characters")]
        public virtual Profession Profession { get; set; }
        [InverseProperty("Character")]
        public virtual ICollection<CharacterImplantInventoryItem> CharacterImplantInventoryItems { get; set; }
        [InverseProperty("Character")]
        public virtual ICollection<CharacterSlottedImplant> CharacterSlottedImplants { get; set; }
        [InverseProperty("Character")]
        public virtual ICollection<ShipCrewSlot> ShipCrewSlots { get; set; }
    }
}