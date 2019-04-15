using System;
using System.Diagnostics;
using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePlanetsDAL.ServiceResponses;
using SpacePlanetsDAL.Services;
using SpLib.Objects;
using WebApp.Models;
using IAuthenticationService = SpacePlanetsDAL.Services.IAuthenticationService;

namespace WebApp.Controllers
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
        
        public IActionResult Index(int? seedno, bool saveSeed)
        {
            string sessionId = _cookie.Get("SpaceRushSession");
            if (seedno == null)
            {
                seedno = 0;
            }
            var viewModel = new GalaxyIndexViewModel(_authenticationService, _objectService, seedno.Value, saveSeed, sessionId);
            return View(viewModel);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
