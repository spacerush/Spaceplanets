using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
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
        
        public AccountController(IGameService gameService, IAuthenticationService authenticationService)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
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
                
            }
            else
            {
                viewModel.Message = "Login was bad.";
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
