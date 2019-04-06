using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Implant
    {
        public Implant()
        {
            CharacterSlottedImplants = new HashSet<CharacterSlottedImplant>();
            ImplantClusters = new HashSet<ImplantCluster>();
        }

        public int ImplantId { get; set; }
        public int ImplantBaseItemId { get; set; }
        public int Quality { get; set; }

        [ForeignKey("ImplantBaseItemId")]
        [InverseProperty("Implants")]
        public virtual ImplantBaseItem ImplantBaseItem { get; set; }
        [InverseProperty("Implant")]
        public virtual CharacterImplantInventoryItem CharacterImplantInventoryItem { get; set; }
        [InverseProperty("Implant")]
        public virtual ICollection<CharacterSlottedImplant> CharacterSlottedImplants { get; set; }
        [InverseProperty("Implant")]
        public virtual ICollection<ImplantCluster> ImplantClusters { get; set; }
    }
}