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

        public PlayerRepository(IMongoClient mongoClient) : base(mongoClient)
        {

            
        }

        public void CenterPlayerCamera(Guid playerId, int x, int y, int z)
        {
            Player player = this.GetOne<Player>(f => f.Id == playerId);
            player.CameraX = x;
            player.CameraY = y;
            player.CameraZ = z;
            this.UpdateOne<Player>(player);
        }

    }
}
