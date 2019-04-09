﻿using System;
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
    public class ItemsController : Controller
    {
        
        private readonly IGameService _gameService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;
        private readonly IObjectService _objectService;

        public ItemsController(IGameService gameService, IAuthenticationService authenticationService, IObjectService objectService, ICookie cookie, ICookieManager cookieManager)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
            _objectService = objectService;
        }
        
        public IActionResult Index()
        {
            string sessionId = _cookie.Get("SpaceRushSession");
            var viewModel = new ItemsIndexViewModel(_authenticationService, _objectService, sessionId);
            return View(viewModel);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}