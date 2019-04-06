using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipTypesResource
    {
        [Key]
        public int ShipTypeResourceId { get; set; }
        public int ShipTypeId { get; set; }
        public int ResourceId { get; set; }
        public int ResourceConsumed { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ShipTypesResources")]
        public virtual Resource Resource { get; set; }
        [ForeignKey("ShipTypeId")]
        [InverseProperty("ShipTypesResources")]
        public virtual ShipType ShipType { get; set; }
    }
}