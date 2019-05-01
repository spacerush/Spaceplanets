using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.Interface;
using SpacePlanetsMvc.Hubs;
using SpacePlanetsMvc.Models;

namespace SpacePlanetsMvc.Controllers
{
    public class HomeController : Controller
    {

        public IHubContext<GalaxyHub, IGalaxyClient> _galaxyHubContext;

        public HomeController(IHubContext<GalaxyHub, IGalaxyClient> hubContext)
        {
            _galaxyHubContext = hubContext;
        }

        public IActionResult Index()
        {
            _galaxyHubContext.Clients.All.ReceiveMessage("Index hit");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
