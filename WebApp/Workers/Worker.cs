using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Hubs;

namespace WebApp.Workers
{
    public class Worker : BackgroundService
    {

        private readonly IHubContext<GalaxyHub, IGalaxyClient> _galaxyHub;

        public Worker(IHubContext<GalaxyHub, IGalaxyClient> galaxyHub)
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
