using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Services
{
    public class LootService : ILootService
    {

        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public LootService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public void SpawnRandomModule(int x, int y, int z)
        {
            ShipModule module = _wrapper.ShipModuleRepository.GetAll<ShipModule>(f => f.Id != null).OrderBy(o => Guid.NewGuid()).Take(1).First();
            SpaceLoot loot = new SpaceLoot();
            loot.X = x;
            loot.Y = y;
            loot.Z = z;
            loot.ShipModules.Add(module);
            _wrapper.SpaceLootRepository.AddOne(loot);
        }

        public List<SpaceLoot> GetAllSpaceLoot(int x, int y, int z)
        {
            var result = new List<SpaceLoot>();
            result.AddRange(_wrapper.SpaceLootRepository.GetAll<SpaceLoot>(f => f.X == x && f.Y == y && f.Z == z));
            return result;
        }

        /// <summary>
        /// Have a ship bring all loot abord by "banking" it for the player.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool TractorAllLoot(Ship ship)
        {
            int itemsLooted = 0;
            var result = _wrapper.SpaceLootRepository.GetAll<SpaceLoot>(f => f.X == ship.X && f.Y == ship.Y && f.Z == ship.Z).ToList();
            foreach (var item in result)
            {
                foreach (var module in item.ShipModules)
                {
                    BankedShipModule lootedModule = new BankedShipModule();
                    lootedModule.PlayerId = ship.PlayerId;
                    lootedModule.ShipId = ship.Id;
                    lootedModule.ShipModule = module;
                    _wrapper.SpaceLootRepository.AddOneAsync<BankedShipModule>(lootedModule);
                    itemsLooted++;
                }
            }
            if (itemsLooted > 0)
            {
                _wrapper.SpaceLootRepository.DeleteMany<SpaceLoot>(result);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TractorSpecificLoot(Ship ship, string itemType, Guid itemId)
        {
            int itemsLooted = 0;
            var result = _wrapper.SpaceLootRepository.GetAll<SpaceLoot>(f => f.Id == itemId && f.X == ship.X && f.Y == ship.Y && f.Z == ship.Z);
            foreach (var item in result)
            {
                foreach (var module in item.ShipModules)
                {
                    BankedShipModule lootedModule = new BankedShipModule();
                    lootedModule.PlayerId = ship.PlayerId;
                    lootedModule.ShipId = ship.Id;
                    lootedModule.ShipModule = module;
                    _wrapper.SpaceLootRepository.AddOneAsync<BankedShipModule>(lootedModule);
                    itemsLooted++;
                }
            }
            if (itemsLooted > 0)
            {
                _wrapper.SpaceLootRepository.DeleteMany<SpaceLoot>(result);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
