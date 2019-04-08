using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using SpLib.Helpers;
using SpLib.Objects;
using SpacePlanetsDAL.ServiceResponses;

namespace SpacePlanetsDAL.Services
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

        public void GetShipsByPlayerId(Guid playerId)
        {

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

        public void GenerateGalaxy(string galaxyName)
        {
            Galaxy galaxy = new Galaxy();
            galaxy.SizeX = _random.Next(50, 200);
            galaxy.SizeY = galaxy.SizeX; //_random.Next(200, 350);
            galaxy.SizeZ = _random.Next(1, 25);
            if (!string.IsNullOrEmpty(galaxyName))
            {
                galaxy.Name = galaxyName;
            }
            else
            {
                galaxy.Name = "Galaxy " + GenerationHelper.CreateRandomString(true, true, false, 5);
            }
            _wrapper.GalaxyRepository.AddOne<Galaxy>(galaxy);
            int volume = galaxy.SizeX * galaxy.SizeY * galaxy.SizeZ;
            int lowerBoundX = (galaxy.SizeX / 2) * -1;
            int lowerBoundY = (galaxy.SizeY / 2) * -1;
            int lowerBoundZ = (galaxy.SizeZ / 2) * -1;
            int upperBoundX = lowerBoundX * -1;
            int upperBoundY = lowerBoundY * -1;
            int upperBoundZ = lowerBoundZ * -1;
            for (int Xctr = lowerBoundX; Xctr <= upperBoundX; Xctr++)
            {
                for (int Yctr = lowerBoundY; Yctr <= upperBoundY; Yctr++)
                {
                    for (int Zctr = lowerBoundZ; Zctr <= upperBoundZ; Zctr++)
                    {
                        // make random number to decide if this is a place a star should go.
                        int starChance = _random.Next(1, 1000);
                        if (starChance <= 2)
                        {
                            // create star
                            StarSystem starSystem = new StarSystem(galaxy.Id);
                            starSystem.Name = "System " + GenerationHelper.CreateRandomString(true, false, false, 6) + " " + GenerationHelper.CreateRandomString(false, true, false, 2);
                            _wrapper.StarSystemRepository.AddOne<StarSystem>(starSystem);
                        }
                    }
                }
            }
            
        }

        public Galaxy RetrieveGalaxyByName(string galaxyName)
        {
            Galaxy galaxy = _wrapper.GalaxyRepository.GetOne<Galaxy>(f => f.Name == galaxyName);
            return galaxy;
        }
    }
}
