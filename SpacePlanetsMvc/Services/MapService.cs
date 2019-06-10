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
            int centerDisplayX = viewWidth / 2;
            int centerDisplayY = viewHeight / 2;

            var result = new GetMapAtShipByShipIdResponse();
            result.MapDataResult = new GetMapDataResult();
            result.MapDataResult.MapDataCells = new List<MapDataCell>();
            Ship ship = _wrapper.ShipRepository.GetOne<Ship>(f => f.Id == shipId);

            // TODO: use a valid scan distance.
            int scanDistance = 25; // hardcoded value for now, the distance from the ship the player and the ship is aware of.     
            

            int minX = ship.X - scanDistance;
            int maxX = ship.X + scanDistance;
            int minY = ship.Y - scanDistance;
            int maxY = ship.Y + scanDistance;
            int minZ = ship.Z - scanDistance;
            int maxZ = ship.Z + scanDistance;
            List<Star> starsForMap = defaultStars.Where(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY && s.Z >= minZ && s.Z <= maxZ).ToList();
            List<Ship> shipsForMap = _wrapper.ShipRepository.GetAll<Ship>(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY && s.Z >= minZ && s.Z <= maxZ).ToList();
            List<SpaceObject> spaceObjectsForMap = _wrapper.SpaceObjectRepository.GetAll<SpaceObject>(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY && s.Z >= minZ && s.Z <= maxZ).ToList();
            List<SpaceLoot> spaceLootsForMap = _wrapper.SpaceLootRepository.GetAll<SpaceLoot>(s => s.X >= minX && s.X <= maxX && s.Y >= minY && s.Y <= maxY && s.Z >= minZ && s.Z <= maxZ).ToList();
            // Take each star that is near the ship and add it to a list of data cells to send to the client.
            foreach (var star in starsForMap)
            {
                int displayAtX = centerDisplayX - (ship.X - star.X);
                int displayAtY = centerDisplayY - (ship.Y - star.Y);
                if (displayAtX >= 0 && displayAtY >= 0 && displayAtX < viewWidth && displayAtY < viewHeight)
                {
                    MapDataCell existingDataCell = result.MapDataResult.MapDataCells.Where(w => w.CellX == displayAtX && w.CellY == displayAtY).FirstOrDefault();
                    if (existingDataCell == null)
                    {
                        MapDataCell newDatacell = new MapDataCell();
                        newDatacell.Stars = new List<Star>();
                        newDatacell.Ships = new List<Ship>();
                        newDatacell.SpaceObjects = new List<SpaceObject>();
                        newDatacell.SpaceLoots = new List<SpaceLoot>();
                        newDatacell.CellX = displayAtX;
                        newDatacell.CellY = displayAtY;
                        newDatacell.CellZ = star.Z;
                        newDatacell.Stars.Add(star);
                        result.MapDataResult.MapDataCells.Add(newDatacell);
                    }
                    else
                    {
                        existingDataCell.Stars.Add(star);
                    }
                }
            }

            // Take each ship that is near the ship (including itself) and send to the client.
            foreach (var shipForMap in shipsForMap)
            {
                int displayAtX = centerDisplayX - (ship.X - shipForMap.X);
                int displayAtY = centerDisplayY - (ship.Y - shipForMap.Y);
                if (displayAtX >= 0 && displayAtY >= 0 && displayAtX < viewWidth && displayAtY < viewHeight)
                {
                    MapDataCell existingDataCell = result.MapDataResult.MapDataCells.Where(w => w.CellX == displayAtX && w.CellY == displayAtY).FirstOrDefault();
                    if (existingDataCell == null)
                    {
                        MapDataCell newDatacell = new MapDataCell();
                        newDatacell.Stars = new List<Star>();
                        newDatacell.Ships = new List<Ship>();
                        newDatacell.SpaceObjects = new List<SpaceObject>();
                        newDatacell.SpaceLoots = new List<SpaceLoot>();
                        newDatacell.CellX = displayAtX;
                        newDatacell.CellY = displayAtY;
                        newDatacell.CellZ = shipForMap.Z;
                        newDatacell.Ships.Add(shipForMap);
                        result.MapDataResult.MapDataCells.Add(newDatacell);
                    }
                    else
                    {
                        existingDataCell.Ships.Add(shipForMap);
                    }
                }
            }

            // Add space objects near the ship.
            foreach (var spaceObject in spaceObjectsForMap)
            {
                int displayAtX = centerDisplayX - (ship.X - spaceObject.X);
                int displayAtY = centerDisplayY - (ship.Y - spaceObject.Y);
                //if (displayAtX >= 0 && displayAtY >= 0 && displayAtX < viewWidth && displayAtY < viewHeight)
                //{
                    MapDataCell existingDataCell = result.MapDataResult.MapDataCells.Where(w => w.CellX == displayAtX && w.CellY == displayAtY).FirstOrDefault();
                    if (existingDataCell == null)
                    {
                        MapDataCell newDatacell = new MapDataCell();
                        newDatacell.Stars = new List<Star>();
                        newDatacell.Ships = new List<Ship>();
                        newDatacell.SpaceObjects = new List<SpaceObject>();
                        newDatacell.CellX = displayAtX;
                        newDatacell.CellY = displayAtY;
                        newDatacell.CellZ = spaceObject.Z;
                        newDatacell.SpaceObjects.Add(spaceObject);
                        result.MapDataResult.MapDataCells.Add(newDatacell);
                    }
                    else
                    {
                        existingDataCell.SpaceObjects.Add(spaceObject);
                    }
                //}
            }

            // Add space objects near the ship.
            foreach (var spaceLoot in spaceLootsForMap)
            {
                int displayAtX = centerDisplayX - (ship.X - spaceLoot.X);
                int displayAtY = centerDisplayY - (ship.Y - spaceLoot.Y);
                //if (displayAtX >= 0 && displayAtY >= 0 && displayAtX < viewWidth && displayAtY < viewHeight)
                //{
                MapDataCell existingDataCell = result.MapDataResult.MapDataCells.Where(w => w.CellX == displayAtX && w.CellY == displayAtY).FirstOrDefault();
                if (existingDataCell == null)
                {
                    MapDataCell newDatacell = new MapDataCell();
                    newDatacell.Stars = new List<Star>();
                    newDatacell.Ships = new List<Ship>();
                    newDatacell.SpaceLoots = new List<SpaceLoot>();
                    newDatacell.CellX = displayAtX;
                    newDatacell.CellY = displayAtY;
                    newDatacell.CellZ = spaceLoot.Z;
                    newDatacell.SpaceLoots.Add(spaceLoot);
                    result.MapDataResult.MapDataCells.Add(newDatacell);
                }
                else
                {
                    existingDataCell.SpaceLoots.Add(spaceLoot);
                }
                //}
            }

            result.Success = true;
            return result;
        }

    }
}
