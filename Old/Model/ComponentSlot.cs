using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ComponentSlot
    {
        public ComponentSlot()
        {
            Components = new HashSet<Component>();
        }

        public int ComponentSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ComponentSlot")]
        public virtual ICollection<Component> Components { get; set; }
    }
}