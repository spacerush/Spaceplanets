using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Ship : Document
    {
        public string Name { get; set; }

        /// <summary>
        /// Should be in degrees per second. Valid values 0 to 360.
        /// </summary>
        public int AngularTurnSpeed { get; set; }

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
        }
    }
}
