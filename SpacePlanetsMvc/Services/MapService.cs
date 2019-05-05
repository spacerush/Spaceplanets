using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Models.ServiceResponses.Map;
using SpacePlanets.SharedModels.ServerToClient;

namespace SpacePlanetsMvc.Services
{
    /// <summary>
    /// Default Implementation of the IMapService.
    /// The purpose of this is to calculate what objects player can interact with, positions etc.
    /// </summary>
    public class MapService : IMapService
    {
        private readonly List<Star> defaultStars;
        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;

        public MapService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            defaultStars = _wrapper.GalaxyContainerRepository.GetOne<GalaxyContainer>(f => f.Name == "Default").Galaxy.Stars.ToList();
        }

        /// <summary>
        /// Gets the map assuming the ship in question should be centered in the view. View is the size of viewWidth x viewHeight
        /// </summary>
        /// <param name="shipId">Guid identifier of ship</param>
        /// <param name="viewWidth"># of tiles wide the player can see at once</param>
        /// <param name="viewHeight"># of tiles high the player can see at once</param>
        /// <returns></returns>
        public GetMapAtShipByShipIdResponse GetMapAtShipByShipId(Guid shipId, int viewWidth, int viewHeight)
        {
            int centerX = viewWidth / 2;
            int centerY = viewHeight / 2;

            var result = new GetMapAtShipByShipIdResponse();
            result.MapDataResult = new GetMapDataResult();
            result.MapDataResult.MapDataCells = new List<MapDataCell>();
            Ship ship = _wrapper.ShipRepository.GetOne<Ship>(f => f.Id == shipId);
            int scanDistance = 20; // hardcoded value for now, the distance from the ship the player can see.
            int minX = ship.X - scanDistance;
            int maxX = ship.X + scanDistance;
            int minY = ship.Y - scanDistance;
            int maxY = ship.Y + scanDistance;
            int minZ = ship.Z - scanDistance;
            int maxZ = ship.Z + scanDistance;
            List<Star> starsForMap = defaultStars.Where(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY && s.Z >= minZ && s.Z <= maxZ).ToList();

        }

    }
}
