using System;
using System.Diagnostics;
using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePlanetsMvc.Models;
using SpacePlanetsMvc.Models.WebViewModels;
using SpacePlanetsMvc.Services;

namespace SpacePlanetsMvc.Controllers
{
    public class GalaxyController : Controller
    {
        
        private readonly IGameService _gameService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;
        private readonly IObjectService _objectService;

        public GalaxyController(IGameService gameService, IAuthenticationService authenticationService, IObjectService objectService, ICookie cookie, ICookieManager cookieManager)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
            _objectService = objectService;
        }
        
        public IActionResult SpaceObjects(int? X, int? Y, int? Z, string objectType, string objectName)
        {
            string sessionId = _cookie.Get("SpacePlanetsSession");
            if (X == null) { X = 0; }
            if (Y == null) { Y = 0; }
            if (Z == null) { Z = 0; }
            var viewModel = new SpaceObjectsViewModel(_authenticationService, _objectService, objectType, objectName, X.Value, Y.Value, Z.Value, sessionId);
            return View(viewModel);
        }

        public IActionResult Index(int? seed, bool saveSeed, string galaxyName)
        {
            string sessionId = _cookie.Get("SpacePlanetsSession");
            if (seed == null)
            {
                seed = 0;
            }
            var viewModel = new GalaxyIndexViewModel(_authenticationService, _objectService, seed.Value, saveSeed, galaxyName, sessionId);
            return View(viewModel);
        }

        public IActionResult Draw()
        {
            var viewModel = new GalaxyDrawViewModel(_objectService);
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
