using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Component
    {
        public Component()
        {
            ModuleComponents = new HashSet<ModuleComponent>();
        }

        public int ComponentId { get; set; }
        public int ModuleBaseItemId { get; set; }
        public int ShipAbilityId { get; set; }
        public int LowestQualityLevel { get; set; }
        public int LowestQualityBuff { get; set; }
        public int HighestQualityLevel { get; set; }
        public int HighestQualityBuff { get; set; }
        public int ComponentSlotId { get; set; }

        [ForeignKey("ComponentSlotId")]
        [InverseProperty("Components")]
        public virtual ComponentSlot ComponentSlot { get; set; }
        [ForeignKey("ModuleBaseItemId")]
        [InverseProperty("Components")]
        public virtual ModuleBaseItem ModuleBaseItem { get; set; }
        [ForeignKey("ShipAbilityId")]
        [InverseProperty("Components")]
        public virtual ShipAbility ShipAbility { get; set; }
        [InverseProperty("Component")]
        public virtual ICollection<ModuleComponent> ModuleComponents { get; set; }
    }
}