using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Shipyard
    {
        public Shipyard()
        {
            ShipyardShipTypes = new HashSet<ShipyardShipType>();
        }

        public int ShipyardId { get; set; }
        public int SpaceObjectId { get; set; }
        public int ShipyardTypeId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("ShipyardTypeId")]
        [InverseProperty("Shipyards")]
        public virtual ShipyardType ShipyardType { get; set; }
        [ForeignKey("SpaceObjectId")]
        [InverseProperty("Shipyards")]
        public virtual SpaceObject SpaceObject { get; set; }
        [InverseProperty("Shipyard")]
        public virtual ICollection<ShipyardShipType> ShipyardShipTypes { get; set; }
    }
}