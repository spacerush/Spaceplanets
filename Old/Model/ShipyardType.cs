using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipyardType
    {
        public ShipyardType()
        {
            Shipyards = new HashSet<Shipyard>();
        }

        public int ShipyardTypeId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ShipyardType")]
        public virtual ICollection<Shipyard> Shipyards { get; set; }
    }
}