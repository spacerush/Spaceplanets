using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipTechnology
    {
        public int ShipTechnologyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}