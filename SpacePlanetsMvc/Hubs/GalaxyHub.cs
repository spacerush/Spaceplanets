using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.ClientToServer;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.Interface;
using SpacePlanets.SharedModels.ServerToClient;
using SpacePlanetsMvc.Models.ServiceResponses.Map;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Hubs
{
    public class GalaxyHub : Hub<IGalaxyClient>
    {
        private readonly IAuthenticationService _authService;
        private readonly IGameService _gameService;
        private readonly IObjectService _objectService;
        private readonly IMapService _mapService;

        public GalaxyHub(IAuthenticationService authService, IObjectService objectService, IGameService gameService, IMapService mapService)
        {
            _authService = authService;
            _gameService = gameService;
            _objectService = objectService;
            _mapService = mapService;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);  // switch to Clients.Others to only send to other people.
        }

        public async Task SendTime()
        {
            await Clients.All.ReceiveServerTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        }

        public async Task GetAccessToken(CredentialsContainer ctr)
        {
            if (_authService.TryLoginCredentials(ctr.Username, ctr.Password))
            {
                AccessToken token = _authService.CreateGameplayToken(ctr.Username);
                var result = new GetAccessTokenResult();
                result.Error = null;
                result.Success = true;
                result.Token = token;
                await Clients.Caller.ReceiveAccessTokenResult(result);
            }
            else
            {
                var result = new GetAccessTokenResult();
                result.Error = new ErrorFromServer("Could not verify credentials.");
                result.Success = false;
                result.Token = null;
                await Clients.Caller.ReceiveAccessTokenResult(result);
            }
        }

        public async Task GetAccessTokenWithRefreshToken(RefreshToken refreshToken)
        {
            var accessToken = _authService.CreateAccessTokenFromRefreshToken(refreshToken.Content);
            if (accessToken != null)
            {
                var result = new GetAccessTokenResult();
                result.Error = null;
                result.Success = true;
                result.Token = accessToken;
                await Clients.Caller.ReceiveAccessTokenFromRefreshToken(result);
            }
        }

        public async Task GetCharactersForMenu(AuthorizationTokenContainer ctr)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetCharactersByPlayerIdResponse getCharactersByPlayerIdResponse = _gameService.GetCharactersByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getCharactersByPlayerIdResponse.Success)
                {
                    var result = new GetCharactersForMenuResult(getCharactersByPlayerIdResponse.Characters);
                    await Clients.Caller.ReceiveCharactersForMenu(result);
                }
            }
        }


        public async Task GetShipsForMenu(AuthorizationTokenContainer ctr)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetShipsByPlayerIdResponse getShipsByPlayerIdResponse = _gameService.GetShipsByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getShipsByPlayerIdResponse.Success)
                {
                    var result = new GetShipsForMenuResult(getShipsByPlayerIdResponse.Ships);
                    await Clients.Caller.ReceiveShipsForMenu(result);
                }
            }
        }

        public async Task Ping(AuthorizationTokenContainer authorizationTokenCtr, PingRequest pingRequest)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(authorizationTokenCtr.Token);
            var result = new PingResponse();
            result.PingId = pingRequest.PingId;
            result.Error = null;
            result.Success = true;
            await Clients.Caller.ReceivePingResponse(result);
        }

        public async Task GetCharacterForManagement(AuthorizationTokenContainer tokenContainer, CharacterForManagementRequest characterForManagementRequest)
        {
            var result = new GetCharacterForManagementResult();
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetCharacterByPlayerIdAndCharacterIdResponse retrievedCharacter = _gameService.GetCharacterByPlayerIdAndCharacter(playerByAccessTokenResponse.Player.Id, characterForManagementRequest.CharacterId);
                if (retrievedCharacter.Success)
                {
                    result.Success = true;
                    result.Character = retrievedCharacter.Character;
                    result.Error = null;
                    await Clients.Caller.ReceiveCharacterForManagement(result);
                }
            }
        }

        public async Task GetPlayerCameraCoordinates(AuthorizationTokenContainer tokenContainer)
        {
            var result = new GetPlayerCameraCoordinatesResult();
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                result.X = playerByAccessTokenResponse.Player.CameraX;
                result.Y = playerByAccessTokenResponse.Player.CameraY;
                result.Z = playerByAccessTokenResponse.Player.CameraZ;
                await Clients.Caller.ReceivePlayerCameraCoordinates(result);
            }
        }

        public async Task GetMapAtShip(AuthorizationTokenContainer tokenContainer, MapAtShipRequest mapAtShipRequestContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, mapAtShipRequestContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    GetMapAtShipByShipIdResponse map = _mapService.GetMapAtShipByShipId(ship.Id, mapAtShipRequestContainer.ViewWidth, mapAtShipRequestContainer.ViewHeight);
                    if (map.Success)
                    {
                        await Clients.Caller.ReceiveMapData(map.MapDataResult);
                    }
                }
            }
        }

        public async Task UpdateShipPosition(AuthorizationTokenContainer tokenContainer, ShipCoordinateContainer shipCoordinateContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, shipCoordinateContainer.ShipId);
                if (serviceResult.Success)
                {
                    _gameService.MoveShip(shipCoordinateContainer.ShipId, shipCoordinateContainer.X, shipCoordinateContainer.Y);
                }
            }
        }

    }

}
