using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class ShipMovementContainer
    {

        public Guid ShipId { get; set; }
        public int ChangeX { get; set; }
        public int ChangeY { get; set; }
        public string ConfirmationId { get; set; }
        public ShipMovementContainer()
        {

        }

    }
}
