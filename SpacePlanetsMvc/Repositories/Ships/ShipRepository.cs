using Marten;
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

        public ShipRepository(IDocumentStore documentStore) : base(documentStore)
        {

            
        }

        public void InitializeShip(Guid shipid, string typeOfShip)
        {
            
            Ship ship = Session.Load<Ship>(shipid);
            ShipTemplate template = Session.Query<ShipTemplate>().Where(f => f.Name == typeOfShip).SingleOrDefault();
            ship.ModuleSlots = template.ModuleSlots;
            ship.ShipModules = new List<ShipModule>();
            ship.Type = typeOfShip;
            GalaxyContainer defaultGalaxy = Session.Query<GalaxyContainer>().Where(f => f.Name == "Default").SingleOrDefault();
            Star star = defaultGalaxy.Galaxy.Stars.OrderBy(o => Guid.NewGuid()).Take(1).SingleOrDefault();
            ship.X = star.X;
            ship.Y = star.Y;
            ship.Z = star.Z;
            Session.Update<Ship>(ship);
        }

        public void PlaceCharacterIn(Guid shipId, Guid characterId)
        {
            Character character = Session.Load<Character>(characterId);
            character.ShipId = shipId;
            Session.Update<Character>(character);
        }

    }
}
