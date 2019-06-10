using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class TakeSpecificLootRequest
    {
        public Guid ShipId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemType { get; set; }

        /// <summary>
        /// Construct a request to take specific loot,
        /// </summary>
        /// <param name="shipId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        public TakeSpecificLootRequest(Guid shipId, Guid itemId, string itemType)
        {
            this.ShipId = shipId;
            this.ItemId = itemId;
            this.ItemType = itemType;
        }

        /// <summary>
        /// Create an empty request to take specific loot.
        /// </summary>
        public TakeSpecificLootRequest()
        {
            this.ShipId = Guid.Empty;
            this.ItemId = Guid.Empty;
        }
    }
}
