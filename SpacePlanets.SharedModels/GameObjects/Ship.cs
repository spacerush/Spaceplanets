using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class Ship : Document
    {

        /// <summary>
        /// The last time the ship moved.
        /// </summary>
        public DateTimeOffset LastMovementUtc { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Should be in degrees per second. Valid values 0 to 360.
        /// </summary>
        public int AngularTurnSpeed { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        /// <summary>
        /// See available ShipTemplates for type validation.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The player a ship belongs to if any.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// List of ship modules, should be the same as this ship type template.
        /// </summary>
        public List<ShipModuleSlot> ModuleSlots { get; set; }

        /// <summary>
        /// List of installed ship modules
        /// </summary>
        public List<ShipModule> ShipModules { get; set; }

        /// <summary>
        /// Create a ship with the given name and type.
        /// </summary>
        /// <param name="name">The name of the ship</param>
        /// <param name="type">The type of the ship (fighter, freighter, etc)</param>
        public Ship(string name, string type)
        {
            Name = name;
            Type = type;
            LastMovementUtc = DateTime.UtcNow;
        }

        public Ship()
        {

        }

        
    }
}
