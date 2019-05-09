using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class ShipCoordinateContainer
    {

        public Guid ShipId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ShipCoordinateContainer()
        {

        }
    }
}
