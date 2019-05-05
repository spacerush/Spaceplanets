using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories.Ships
{
    public class ShipRepository : RepositoryBase<Ship>, IShipRepository
    {

        public ShipRepository(IMongoClient mongoClient) : base(mongoClient)
        {

            
        }

        public void InitializeShip(Guid shipid, string typeOfShip)
        {
            Ship ship = this.GetOne<Ship>(f => f.Id == shipid);
            ShipTemplate template = this.GetOne<ShipTemplate>(f => f.Name == typeOfShip);
            ship.ModuleSlots = template.ModuleSlots;
            ship.ShipModules = new List<ShipModule>();
            ship.Type = typeOfShip;
            this.UpdateOne<Ship>(ship);
        }

    }
}
