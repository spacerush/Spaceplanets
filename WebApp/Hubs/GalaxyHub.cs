using Microsoft.AspNetCore.SignalR;
using SpacePlanetsDAL.ServiceResponses;
using SpacePlanetsDAL.Services;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class GalaxyHub : Hub<IGalaxyClient>
    {

        private readonly IGameService _gameService;
        private readonly IObjectService _objectService;
        private readonly IAuthenticationService _authenticationService;
        public GalaxyHub(IGameService gameService, IAuthenticationService authenticationService, IObjectService objectService)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
            _objectService = objectService;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);  // switch to Clients.Others to only send to other people.
        }

        public async Task GetCharactersForMenu(AuthorizationTokenContainer authorizationTokenContainer)
        {
            var result = new GetCharactersForMenuResult();
            if (authorizationTokenContainer != null)
            {
                GetPlayerByAccessTokenResponse getPlayerByAccessTokenResponse = _authenticationService.GetPlayerByAccessToken(authorizationTokenContainer.Content);
                if (getPlayerByAccessTokenResponse.Success)
                {
                    List<GenericItemForPicklist> characters = new List<GenericItemForPicklist>();
                    GetCharactersByPlayerIdResponse getCharactersByPlayerIdResponse = _gameService.GetCharactersByPlayerId(getPlayerByAccessTokenResponse.Player.Id);
                    if (getCharactersByPlayerIdResponse.Success == true)
                    {
                        foreach (var item in getCharactersByPlayerIdResponse.Characters)
                        {
                            characters.Add(new GenericItemForPicklist(item.Id, item.Name + ", level " + item.Level + " " + item.Profession));
                        }
                    }
                    result.Characters = characters;
                    result.Error = null;
                    result.Success = true;
                }
                else
                {
                    result.Error = new ErrorFromServer("Could not translate token into player for GetCharactersForMenu.");
                    result.Characters = null;
                    result.Success = false;
                }
            }
            await Clients.Caller.ReceiveCharactersForMenu(result);
        }
    }
}
