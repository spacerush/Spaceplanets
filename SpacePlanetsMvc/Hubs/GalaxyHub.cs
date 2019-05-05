using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.ClientToServer;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.Interface;
using SpacePlanets.SharedModels.ServerToClient;
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
        
        public GalaxyHub(IAuthenticationService authService, IObjectService objectService, IGameService gameService)
        {
            _authService = authService;
            _gameService = gameService;
            _objectService = objectService;
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


    }
}
