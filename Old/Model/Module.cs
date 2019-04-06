using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Module
    {
        public Module()
        {
            ModuleComponents = new HashSet<ModuleComponent>();
        }

        public int ModuleId { get; set; }
        public int ModuleBaseItemId { get; set; }
        public int Quality { get; set; }

        [ForeignKey("ModuleBaseItemId")]
        [InverseProperty("Modules")]
        public virtual ModuleBaseItem ModuleBaseItem { get; set; }
        [InverseProperty("Module")]
        public virtual ICollection<ModuleComponent> ModuleComponents { get; set; }
    }
}