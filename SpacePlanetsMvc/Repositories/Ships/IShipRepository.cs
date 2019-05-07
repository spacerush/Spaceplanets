using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories.Ships
{
    public interface IShipRepository : IRepositoryBase<Ship>
    {
        void InitializeShip(Guid shipid, string typeOfShip);

        void PlaceCharacterIn(Guid shipId, Guid characterId);
    }
}
