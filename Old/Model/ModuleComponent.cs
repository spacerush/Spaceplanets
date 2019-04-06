using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ModuleComponent
    {
        public int ModuleComponentId { get; set; }
        public int ModuleId { get; set; }
        public int ComponentId { get; set; }

        [ForeignKey("ComponentId")]
        [InverseProperty("ModuleComponents")]
        public virtual Component Component { get; set; }
        [ForeignKey("ModuleId")]
        [InverseProperty("ModuleComponents")]
        public virtual Module Module { get; set; }
    }
}