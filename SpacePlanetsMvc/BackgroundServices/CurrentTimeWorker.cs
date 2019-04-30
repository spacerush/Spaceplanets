using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SpacePlanets.SharedModels.Interface;
using SpacePlanetsMvc.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.BackgroundServices
{
    public class CurrentTimeWorker : BackgroundService
    {

        private readonly IHubContext<GalaxyHub, IGalaxyClient> _galaxyHub;

        public CurrentTimeWorker(IHubContext<GalaxyHub, IGalaxyClient> galaxyHub)
        {
            _galaxyHub = galaxyHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _galaxyHub.Clients.All.ReceiveServerTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                await Task.Delay(1000);
            }
        }
    }
}
