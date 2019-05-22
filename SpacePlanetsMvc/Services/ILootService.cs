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

        List<SpaceLoot> GetAllSpaceLoot(int x, int y, int z);
    }
}
