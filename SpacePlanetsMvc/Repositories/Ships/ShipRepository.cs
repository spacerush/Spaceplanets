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
            GalaxyContainer defaultGalaxy = this.GetOne<GalaxyContainer>(f => f.Name == "Default");
            Star star = defaultGalaxy.Galaxy.Stars.OrderBy(o => Guid.NewGuid()).Take(1).SingleOrDefault();
            ship.X = star.X;
            ship.Y = star.Y;
            ship.Z = star.Z;
            this.UpdateOne<Ship>(ship);
        }

        public void PlaceCharacterIn(Guid shipId, Guid characterId)
        {
            Character character = this.GetOne<Character>(f => f.Id == characterId);
            character.ShipId = shipId;
            this.UpdateOne<Character>(character);
        }

    }
}
