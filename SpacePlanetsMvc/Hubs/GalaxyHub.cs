using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Hubs
{
    public class GalaxyHub : Hub<IGalaxyClient>
    {
        public GalaxyHub()
        {
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);  // switch to Clients.Others to only send to other people.
        }

        public async Task SendTime()
        {
            await Clients.All.ReceiveServerTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        }
    }
}
