using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class TakeSpecificLootRequest
    {
        public Guid ShipId { get; set; }
        public List<Guid> ShipModules { get; set; }

        /// <summary>
        /// Construct a request to take specific loot,
        /// with the ship id and a single module populated into the list
        /// </summary>
        /// <param name="shipId"></param>
        /// <param name="moduleId"></param>
        public TakeSpecificLootRequest(Guid shipId, Guid moduleId)
        {
            this.ShipId = shipId;
            this.ShipModules = new List<Guid>();
            this.ShipModules.Add(moduleId);
        }

        /// <summary>
        /// Create an empty request to take specific loot.
        /// </summary>
        public TakeSpecificLootRequest()
        {
            this.ShipId = Guid.Empty;
            this.ShipModules = new List<Guid>();
        }
    }
}
