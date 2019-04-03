using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipAbility
    {
        public ShipAbility()
        {
            Components = new HashSet<Component>();
        }

        public int ShipAbilityId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ShipAbility")]
        public virtual ICollection<Component> Components { get; set; }
    }
}