using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ModuleSlot
    {
        public ModuleSlot()
        {
            ModuleBaseItems = new HashSet<ModuleBaseItem>();
        }

        public int ModuleSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ModuleSlot")]
        public virtual ICollection<ModuleBaseItem> ModuleBaseItems { get; set; }
    }
}