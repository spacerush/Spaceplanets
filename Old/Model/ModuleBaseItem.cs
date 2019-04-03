using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ModuleBaseItem
    {
        public ModuleBaseItem()
        {
            Components = new HashSet<Component>();
            Modules = new HashSet<Module>();
        }

        public int ModuleBaseItemId { get; set; }
        public int ModuleSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int MinQuality { get; set; }
        public int MaxQuality { get; set; }

        [ForeignKey("ModuleSlotId")]
        [InverseProperty("ModuleBaseItems")]
        public virtual ModuleSlot ModuleSlot { get; set; }
        [InverseProperty("ModuleBaseItem")]
        public virtual ICollection<Component> Components { get; set; }
        [InverseProperty("ModuleBaseItem")]
        public virtual ICollection<Module> Modules { get; set; }
    }
}