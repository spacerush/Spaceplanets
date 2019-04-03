using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ShipyardShipType
    {
        public int ShipyardShipTypeId { get; set; }
        public int ShipyardId { get; set; }
        public int ShipTypeId { get; set; }

        [ForeignKey("ShipTypeId")]
        [InverseProperty("ShipyardShipTypes")]
        public virtual ShipType ShipType { get; set; }
        [ForeignKey("ShipyardId")]
        [InverseProperty("ShipyardShipTypes")]
        public virtual Shipyard Shipyard { get; set; }
    }
}