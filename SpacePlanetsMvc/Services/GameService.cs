﻿using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
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
    }
}