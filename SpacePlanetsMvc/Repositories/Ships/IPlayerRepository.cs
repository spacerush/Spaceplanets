using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Repositories.Ships
{
    public interface IPlayerRepository : IRepositoryBase<Player>
    {
        void CenterPlayerCamera(Guid playerId, int x, int y, int z);

    }
}
