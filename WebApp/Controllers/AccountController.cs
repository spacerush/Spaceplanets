using System;
using System.Diagnostics;
using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpacePlanetsDAL.Services;
using SpLib.Objects;
using WebApp.Models;
using IAuthenticationService = SpacePlanetsDAL.Services.IAuthenticationService;

namespace WebApp.Controllers
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
            viewModel.Message = "Cookie is : " + _cookie.Get("SpaceRushSession");
            return View(viewModel);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _cookie.Remove("SpaceRushSession");
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string spusername, string sppassword)
        {
            var viewModel = new DoLoginModel();
            
            if (_authenticationService.TryLoginCredentials(spusername, sppassword))
            {
                WebSession session = _authenticationService.CreateWebSession(spusername);
                viewModel.Message = "Created new web session: " + session.SessionCookie;
                _cookie.Set("SpaceRushSession", session.SessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) });
            }
            else
            {
                viewModel.Message = "Login was bad.";
                _cookie.Remove("SpaceRushSession");
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
