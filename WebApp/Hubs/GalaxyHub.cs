using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class GalaxyHub : Hub
    {
        public async Task SendChat(string message)
        {
            await Clients.Others.SendAsync("chat", message);
        }
    }
}
