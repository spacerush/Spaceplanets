using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpacePlanetsDAL.Services;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;

namespace WebApp.Controllers
{
    [Route("api")]
    public class RpcController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IAuthenticationService _authenticationService;

        public RpcController(IGameService gameService, IAuthenticationService authenticationService)
        {
            _gameService = gameService;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Given a certain username and password, return a token that is good for an hour.
        /// </summary>
        /// <param name="u">Username</param>
        /// <param name="p">Password</param>
        /// <returns>A JSON access token.</returns>
        [Route("Rpc/GetToken")]
        [HttpPost]
        public JsonResult GetToken([FromBody] LoginInformation loginInformation)
        {
            if (_authenticationService.TryLoginCredentials(loginInformation.U, loginInformation.P))
            {
                AccessToken token = _authenticationService.CreateGameplayToken(loginInformation.U);
                var result = new JsonResult(token);
                result.StatusCode = 200;
                return result;
            }
            else
            {
                var result = new JsonResult(new ErrorFromServer("An incorrect password was supplied."));
                result.StatusCode = 403;
                return result;
            }
        }
    }
}