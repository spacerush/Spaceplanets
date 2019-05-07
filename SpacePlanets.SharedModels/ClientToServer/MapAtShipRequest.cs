using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class MapAtShipRequest
    {
        public Guid ShipId { get; set; }
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
    }
}
