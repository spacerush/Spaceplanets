using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Services
{
    public interface ILootService
    {
        void SpawnRandomModule(int x, int y, int z);

        /// <summary>
        /// This gets a list of space loot at certain coordinates. Useful for maps, scans, etc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        List<SpaceLoot> GetAllSpaceLoot(int x, int y, int z);

        /// <summary>
        /// This disappears all loot and places banked loot in a ship
        /// </summary>
        /// <param name="ship"></param>
        bool TractorAllLoot(Ship ship);


        bool TractorSpecificLoot(Ship ship, string itemType, Guid itemId);
    }
}
