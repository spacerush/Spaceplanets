using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpacePlanetsMvc.Services
{
    public class GameService : IGameService
    {
        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public GameService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public GetShipsByPlayerIdResponse GetShipsByPlayerId(Guid playerId)
        {
            var result = new GetShipsByPlayerIdResponse();
            result.Ships = new List<Ship>();
            result.Ships.AddRange(_wrapper.ShipRepository.GetAll<Ship>(f => f.PlayerId == playerId));
            if (result.Ships.Count > 0)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
            result.PlayerId = playerId;
            return result;
        }

        public GetShipsByPlayerIdResponse GetShipByPlayerId(Guid playerId, Guid shipId)
        {
            var result = new GetShipsByPlayerIdResponse();
            result.Ships = new List<Ship>();
            Ship ship = _wrapper.ShipRepository.GetOne<Ship>(f => f.Id == shipId);
            if (ship.PlayerId == playerId)
            {
                result.Ships.Add(ship);

                if (result.Ships.Count == 1)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                }
                result.PlayerId = playerId;
            }
            return result;
        }

        public GetCharactersByPlayerIdResponse GetCharactersByPlayerId(Guid playerId)
        {
            var result = new GetCharactersByPlayerIdResponse();
            result.Characters = new List<Character>();
            result.Characters.AddRange(_wrapper.CharacterRepository.GetAll<Character>(f => f.PlayerId == playerId));
            if (result.Characters.Count > 0)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }
            result.PlayerId = playerId;
            return result;
        }

        public GetCharacterByPlayerIdAndCharacterIdResponse GetCharacterByPlayerIdAndCharacter(Guid playerId, Guid characterId)
        {
            var result = new GetCharacterByPlayerIdAndCharacterIdResponse();
            Character character = _wrapper.CharacterRepository.GetOne<Character>(f => f.Id == characterId && f.PlayerId == playerId);
            if (character != null)
            {
                result.Character = character;
                result.Success = true;
                result.CharacterId = character.Id;
                result.PlayerId = character.PlayerId;
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        public void GenerateGalaxy(string galaxyName)
        {
            
            
        }

        public GalaxyContainer RetrieveGalaxyContainerByName(string galaxyContainerName)
        {
            GalaxyContainer galaxyContainer = _wrapper.GalaxyContainerRepository.GetOne<GalaxyContainer>(f => f.Name == galaxyContainerName);
            return galaxyContainer;
        }

        public void MoveShip(Guid shipId, int x, int y)
        {
            Ship ship = _wrapper.ShipRepository.GetOne<Ship>(f => f.Id == shipId);
            ship.X = x;
            ship.Y = y;
            // check for warpgate at the new location of the ship, if there are a warpgate move the ship again!
            // TODO: extract this into a method of its own (keep it dry). See line ~139 and ~140 for repeated code.
            List<SpaceObject> warpgates = _wrapper.SpaceObjectRepository.GetAll<SpaceObject>(f => f.X == ship.X && f.Y == ship.Y && f.Z == ship.Z && f.ObjectType == "Warpgate").ToList();
            if (warpgates.Count == 1)
            {
                Guid destinationId = warpgates.First().DestinationSpaceObjectId;
                if (destinationId != Guid.Empty)
                {
                    SpaceObject destinationObject = _wrapper.SpaceObjectRepository.GetById<SpaceObject>(destinationId);
                    ship.X = destinationObject.X;
                    ship.Y = destinationObject.Y;
                    ship.Z = destinationObject.Z;
                }
            }
            ship.LastMovementUtc = DateTime.UtcNow;
            _wrapper.ShipRepository.UpdateOne<Ship>(ship);
        }

        public void MoveShipRelative(Guid shipId, int changeX, int changeY)
        {
            Ship ship = _wrapper.ShipRepository.GetOne<Ship>(f => f.Id == shipId);
            ship.X = ship.X + changeX;
            ship.Y = ship.Y + changeY;
            // check for warpgate at the new location of the ship, if there are a warpgate move the ship again!
            // TODO: extract this into a method of its own (keep it dry). See line ~139 and ~140 for repeated code.
            List<SpaceObject> warpgates = _wrapper.SpaceObjectRepository.GetAll<SpaceObject>(f => f.X == ship.X && f.Y == ship.Y && f.Z == ship.Z && f.ObjectType == "Warpgate").ToList();
            if (warpgates.Count == 1)
            {
                Guid destinationId = warpgates.First().DestinationSpaceObjectId;
                if (destinationId != Guid.Empty)
                {
                    SpaceObject destinationObject = _wrapper.SpaceObjectRepository.GetById<SpaceObject>(destinationId);
                    ship.X = destinationObject.X;
                    ship.Y = destinationObject.Y;
                    ship.Z = destinationObject.Z;
                }
            }
            ship.LastMovementUtc = DateTime.UtcNow;
            _wrapper.ShipRepository.UpdateOne<Ship>(ship);
        }
    }
}
