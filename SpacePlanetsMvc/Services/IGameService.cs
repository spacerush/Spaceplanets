using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Get all ships owned by a certain player.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        GetShipsByPlayerIdResponse GetShipsByPlayerId(Guid playerId);

        /// <summary>
        /// Gets a single ship owned by a certain player, but returns it as the sole member of what could be a list of ships.
        /// </summary>
        /// <param name="playerId">The playerId in question</param>
        /// <param name="shipId">The shipId being searched for</param>
        GetShipsByPlayerIdResponse GetShipByPlayerId(Guid playerId, Guid shipId);

        /// <summary>
        /// Get all the characters owned by a certain player.
        /// </summary>
        /// <param name="playerId">The unique identifier of the player.</param>
        GetCharactersByPlayerIdResponse GetCharactersByPlayerId(Guid playerId);

        /// <summary>
        /// Persist a galaxy to the database.
        /// </summary>
        /// <param name="galaxyName">The name of the galaxy to save.</param>
        void GenerateGalaxy(string galaxyName);

        /// <summary>
        /// Retrieve a galaxy based on its name.
        /// </summary>
        /// <param name="galaxyName">The name of the galaxy.</param>
        /// <returns>A galaxy</returns>
        GalaxyContainer RetrieveGalaxyContainerByName(string galaxyName);

        /// <summary>
        /// Gets a single character as long as it matches the given owner.
        /// </summary>
        /// <param name="playerId">The unique identifier of the player (owner)</param>
        /// <param name="characterId">The unique identifier of the character to retrieve.</param>
        /// <returns>A container object</returns>
        GetCharacterByPlayerIdAndCharacterIdResponse GetCharacterByPlayerIdAndCharacter(Guid playerId, Guid characterId);
        void MoveShip(Guid shipId, int x, int y);

        void MoveShipRelative(Guid shipId, int changeX, int changeY);
    }
}
