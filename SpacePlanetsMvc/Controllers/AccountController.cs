using System;
using System.Diagnostics;
using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanetsMvc.Models;
using SpacePlanetsMvc.Models.WebViewModels;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Services;

namespace SpacePlanetsMvc.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IGameService _gameService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie _cookie;
        private readonly ICookieManager _cookieManager;

        public AccountController(IGameService gameService, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
            _cookie = cookie;
            _cookieManager = cookieManager;
        }
        
        public IActionResult Index()
        {
            var viewModel = new AccountIndexViewModel();
            string sessionId = _cookie.Get("SpacePlanetsSession");
            if (string.IsNullOrEmpty(sessionId)) {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                GetPlayerByCookieResponse playerByCookie = _authenticationService.GetPlayerByWebCookie(sessionId);
                if (playerByCookie.Success == true)
                {
                    viewModel.Message = "Welcome, " + playerByCookie.Player.Username;
                    viewModel.GetCharactersByPlayerIdResponse = _gameService.GetCharactersByPlayerId(playerByCookie.Player.Id);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            _cookie.Remove("SpacePlanetsSession");
            return View();
        }

        public IActionResult Logout()
        {
            _cookie.Remove("SpacePlanetsSession");
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string spusername, string sppassword)
        {
            var viewModel = new DoLoginModel();
            
            if (_authenticationService.TryLoginCredentials(spusername, sppassword))
            {
                WebSession session = _authenticationService.CreateWebSession(spusername);
                viewModel.Message = "Created new web session valid until " + session.Expiry.ToShortDateString();
                _cookie.Set("SpacePlanetsSession", session.SessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
