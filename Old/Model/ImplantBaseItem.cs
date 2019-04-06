using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ImplantBaseItem
    {
        public ImplantBaseItem()
        {
            Clusters = new HashSet<Cluster>();
            Implants = new HashSet<Implant>();
        }

        public int ImplantBaseItemId { get; set; }
        public int ImplantSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int MinQuality { get; set; }
        public int MaxQuality { get; set; }

        [ForeignKey("ImplantSlotId")]
        [InverseProperty("ImplantBaseItems")]
        public virtual ImplantSlot ImplantSlot { get; set; }
        [InverseProperty("ImplantBaseItem")]
        public virtual ICollection<Cluster> Clusters { get; set; }
        [InverseProperty("ImplantBaseItem")]
        public virtual ICollection<Implant> Implants { get; set; }
    }
}