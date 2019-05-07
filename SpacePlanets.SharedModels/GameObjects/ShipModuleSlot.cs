using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class ShipModuleSlot
    {
        /// <summary>
        /// Denotes power plant, gun, etc. See ShipTemplates for valid values.
        /// </summary>
        public string SlotType { get; set; }
        /// <summary>
        /// The maximum level of a module
        /// </summary>
        public int MaxLevel { get; set; }

        /// <summary>
        /// The minimum level a module can be. Often 1.
        /// </summary>
        public int MinLevel { get; set; }

        /// <summary>
        /// Creates a record of a given module able to be populated on a ship or template.
        /// </summary>
        /// <param name="minLevel">Desired minimum module level.</param>
        /// <param name="maxLevel">Desired maximum module level.</param>
        /// <param name="slotType">Desired type of slot</param>
        public ShipModuleSlot(int minLevel, int maxLevel, string slotType)
        {
            SlotType = slotType;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }

        public ShipModuleSlot()
        {

        }
    }
}
