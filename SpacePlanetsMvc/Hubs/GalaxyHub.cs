using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.ClientToServer;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.Interface;
using SpacePlanets.SharedModels.ServerToClient;
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

        public GalaxyHub(IAuthenticationService authService)
        {
            _authService = authService;
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

        }
    }
}
