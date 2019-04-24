using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpacePlanetsDAL.Services;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using Flurl;
using Flurl.Http;
using SpLib.Glances;
using SpacePlanetsClientLib.Results;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using SpacePlanetsDAL.ServiceResponses;
using SpLib.Shared;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Controllers
{
    [Route("api")]
    public class RpcController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IObjectService _objectService;
        private IHubContext<GalaxyHub, IGalaxyClient> _hubContext;

        private readonly IAuthenticationService _authenticationService;
        private static HttpClient Client = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.None
        });

        public RpcController(IGameService gameService, IAuthenticationService authenticationService, IObjectService objectService, IHubContext<GalaxyHub, IGalaxyClient> hubContext)
        {
            _hubContext = hubContext;
            _gameService = gameService;
            _authenticationService = authenticationService;
            _objectService = objectService;
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

        /// <summary>
        /// Given a certain username and password, return a token that is good for an hour.
        /// </summary>
        /// <param name="u">Username</param>
        /// <param name="p">Password</param>
        /// <returns>A JSON access token.</returns>
        [Route("Rpc/GetTokenUsingRefreshToken")]
        [HttpPost]
        public JsonResult GetTokenUsingRefreshToken([FromBody] RefreshTokenContainer refreshTokenContainer)
        {
            if (refreshTokenContainer != null)
            {
                var accessToken = _authenticationService.CreateAccessTokenFromRefreshToken(refreshTokenContainer.Content);
                if (accessToken != null)
                {
                    var accessTokenResult = new JsonResult(accessToken);
                    accessTokenResult.StatusCode = 200;
                    return accessTokenResult;
                }
            }
            var result = new JsonResult(new ErrorFromServer("An incorrect refresh token was supplied."));
            result.StatusCode = 403;
            return result;
        }

        [Route("Rpc/Ping")]
        [HttpPost]
        public JsonResult Ping([FromBody] PingRequest pingRequest)
        {
            if (pingRequest != null)
            {
                _hubContext.Clients.All.ReceiveMessage("pinged!");
                var pingResponse = new PingResponse();
                pingResponse.Error = null;
                pingResponse.OriginalDateTime = pingRequest.DateTime;
                pingResponse.ResponseDateTime = DateTime.UtcNow;
                pingResponse.Success = true;
                var jsonResponse = new JsonResult(pingResponse);
                jsonResponse.StatusCode = 200;
                return jsonResponse;
            }
            var result = new JsonResult(new ErrorFromServer("Ping request failure."));
            result.StatusCode = 403;
            return result;
        }

        [Route("Rpc/GetGalaxyByName")]
        [HttpPost]
        public JsonResult GetGalaxyByName([FromBody] AuthorizationTokenContainer authorizationTokenContainer, [FromQuery] string galaxyName)
        {
            JsonResult result;

            if (authorizationTokenContainer != null)
            {
                GetPlayerByAccessTokenResponse getPlayerByAccessTokenResponse = _authenticationService.GetPlayerByAccessToken(authorizationTokenContainer.Content);
                if (getPlayerByAccessTokenResponse.Success)
                {
                    result = new JsonResult(_objectService.GetGalaxyContainer(galaxyName));
                    result.StatusCode = 200;
                    return result;
                }
            }
            result = new JsonResult(new ErrorFromServer("Could not retrieve galaxy."));
            result.StatusCode = 501;
            return result;
        }

        [Route("Character/{id}")]
        [HttpGet]
        public JsonResult Character([FromHeader] string authorization, Guid id)
        {
            GetCharacterForManagementResult getCharacterForManagementResult = new GetCharacterForManagementResult();
            var serviceResponse = new JsonResult(getCharacterForManagementResult);

            if (!string.IsNullOrEmpty(authorization))
            {
                GetPlayerByAccessTokenResponse getPlayerByAccessTokenResponse = _authenticationService.GetPlayerByAccessToken(authorization);
                if (getPlayerByAccessTokenResponse.Success)
                {
                    GetCharacterByPlayerIdAndCharacterIdResponse getCharacterByPlayerIdAndCharacterIdResponse = _gameService.GetCharacterByPlayerIdAndCharacter(getPlayerByAccessTokenResponse.Player.Id, id);
                    if (getCharacterByPlayerIdAndCharacterIdResponse.Success == true)
                    {
                        getCharacterForManagementResult.Success = true;
                        getCharacterForManagementResult.Error = null;
                        getCharacterForManagementResult.Character = getCharacterByPlayerIdAndCharacterIdResponse.Character;

                    }
                    else
                    {
                        getCharacterForManagementResult.Success = false;
                        getCharacterForManagementResult.Character = null;
                        getCharacterForManagementResult.Error = new ErrorFromServer("You cannot retrieve the requested character.");
                    }
                }
                if (getCharacterForManagementResult.Success)
                {
                    serviceResponse.StatusCode = 200;
                }
                else
                {
                    serviceResponse.StatusCode = 403;
                }
            }
            else
            {
                serviceResponse.StatusCode = 404;
                getCharacterForManagementResult.Success = false;
                getCharacterForManagementResult.Character = null;
                getCharacterForManagementResult.Error = new ErrorFromServer("You cannot retrieve a list of characters.");
            }
            return serviceResponse;
        }


        /// <summary>
        /// Gets memory use on the server using the glances api (if installed)
        /// </summary>
        /// <returns></returns>
        [Route("Rpc/GetMemUse")]
        [HttpGet]
        public JsonResult GetMemUse()
        {
            try
            {
                FlurlClient client = new FlurlClient(Client);
                var obj = "http://localhost:61208/api/3/mem"
                    .WithHeader("Accept-Encoding", "identity")
                    .WithClient(client)
                .GetJsonAsync<Mem>().Result;
                GetMemUseResult dto = new GetMemUseResult(true, obj);
                return new JsonResult(dto);
            }
            catch (Exception ex)
            {
                var result = new JsonResult(new ErrorFromServer("Memory information could not be retrieved."));
                result.StatusCode = 501;
                return result;
            }
        }
        
        /// <summary>
        /// Gets cpu use on the server using glances api (if installed)
        /// </summary>
        /// <returns></returns>
        [Route("Rpc/GetCpuUse")]
        [HttpGet]
        public JsonResult GetCpuUse()
        {
            try
            {
                FlurlClient client = new FlurlClient(Client);
                var obj = "http://localhost:61208/api/3/cpu"
                    .WithHeader("Accept-Encoding", "identity")
                    .WithClient(client)
                .GetJsonAsync<Cpu>().Result;
                GetCpuUseResult dto = new GetCpuUseResult(true, obj);
                return new JsonResult(dto);
            }
            catch (Exception ex)
            {
                var result = new JsonResult(new ErrorFromServer("Cpu information could not be retrieved."));
                result.StatusCode = 501;
                return result;
            }
        }


    }
}