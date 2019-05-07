using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
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
        public IServiceProvider Services { get; }

        public CurrentTimeWorker(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedAuthService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
                    IHubContext<GalaxyHub, IGalaxyClient> scopedHubContext = scope.ServiceProvider.GetRequiredService<IHubContext<GalaxyHub, IGalaxyClient>>();
                    await scopedHubContext.Clients.All.ReceiveServerTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                }
                await Task.Delay(1000);
            }
        }
    }
}
