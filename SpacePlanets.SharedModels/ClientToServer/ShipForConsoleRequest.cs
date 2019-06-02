using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class ShipForConsoleRequest
    {
        public Guid ShipId { get; set; }

        /// <summary>
        /// Empty constructor needed for serialization
        /// </summary>
        public ShipForConsoleRequest()
        {

        }


        /// <summary>
        /// Construct and set the ship id.
        /// </summary>
        /// <param name="shipId">The key of the ship in question.</param>
        public ShipForConsoleRequest(Guid shipId)
        {
            this.ShipId = shipId;
        }
    }
}
