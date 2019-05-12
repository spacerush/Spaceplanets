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
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {

        public PlayerRepository(IDocumentStore documentStore) : base(documentStore)
        {

            
        }

        public void CenterPlayerCamera(Guid playerId, int x, int y, int z)
        {
            Player player = Session.Load<Player>(playerId);
            player.CameraX = x;
            player.CameraY = y;
            player.CameraZ = z;
            Session.Update<Player>(player);
        }

    }
}
