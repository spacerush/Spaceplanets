using Microsoft.AspNetCore.SignalR;
using SpacePlanets.SharedModels.ClientToServer;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.Interface;
using SpacePlanets.SharedModels.ServerToClient;
using SpacePlanetsMvc.Models.ServiceResponses;
using SpacePlanetsMvc.Models.ServiceResponses.Map;
using SpacePlanetsMvc.ServiceResponses;
using SpacePlanetsMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Hubs
{
    public class GalaxyHub : Hub<IGalaxyClient>
    {
        private readonly IAuthenticationService _authService;
        private readonly IGameService _gameService;
        private readonly IObjectService _objectService;
        private readonly IMapService _mapService;
        private readonly ILootService _lootService;

        public GalaxyHub(IAuthenticationService authService, IObjectService objectService, IGameService gameService, IMapService mapService, ILootService lootService)
        {
            _lootService = lootService;
            _authService = authService;
            _gameService = gameService;
            _objectService = objectService;
            _mapService = mapService;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);  // switch to Clients.Others to only send to other people.
        }

        public async Task SendTime()
        {
            await Clients.All.ReceiveServerTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        }

        public async Task GetAccessToken(CredentialsContainer ctr)
        {
            if (_authService.TryLoginCredentials(ctr.Username, ctr.Password))
            {
                AccessToken token = _authService.CreateGameplayToken(ctr.Username);
                var result = new GetAccessTokenResult();
                result.Error = null;
                result.Success = true;
                result.Token = token;
                await Clients.Caller.ReceiveAccessTokenResult(result);
            }
            else
            {
                var result = new GetAccessTokenResult();
                result.Error = new ErrorFromServer("Could not verify credentials.");
                result.Success = false;
                result.Token = null;
                await Clients.Caller.ReceiveAccessTokenResult(result);
            }
        }

        public async Task GetAccessTokenWithRefreshToken(RefreshToken refreshToken)
        {
            var accessToken = _authService.CreateAccessTokenFromRefreshToken(refreshToken.Content);
            if (accessToken != null)
            {
                var result = new GetAccessTokenResult();
                result.Error = null;
                result.Success = true;
                result.Token = accessToken;
                await Clients.Caller.ReceiveAccessTokenFromRefreshToken(result);
            }
        }

        public async Task GetCharactersForMenu(AuthorizationTokenContainer ctr)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetCharactersByPlayerIdResponse getCharactersByPlayerIdResponse = _gameService.GetCharactersByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getCharactersByPlayerIdResponse.Success)
                {
                    var result = new GetCharactersForMenuResult(getCharactersByPlayerIdResponse.Characters);
                    await Clients.Caller.ReceiveCharactersForMenu(result);
                }
            }
        }


        public async Task GetShipsForMenu(AuthorizationTokenContainer ctr)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetShipsByPlayerIdResponse getShipsByPlayerIdResponse = _gameService.GetShipsByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getShipsByPlayerIdResponse.Success)
                {
                    var result = new GetShipsForMenuResult(getShipsByPlayerIdResponse.Ships);
                    await Clients.Caller.ReceiveShipsForMenu(result);
                }
            }
        }


        /// <summary>
        /// Given a specific ship contained within the req object and owned by the player,
        /// vacuum up all the loot at the given ship's coordinates.
        /// </summary>
        /// <param name="ctr">A container containing authorization information</param>
        /// <param name="req">A request containing the ship id of the ship grabbing loot</param>
        public async Task TakeAllLoot(AuthorizationTokenContainer ctr, TakeAllLootRequest req)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetShipsByPlayerIdResponse getShipsByPlayerIdResponse = _gameService.GetShipsByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getShipsByPlayerIdResponse.Success)
                {
                    Ship ship = getShipsByPlayerIdResponse.Ships.Where(x => x.Id == req.ShipId).SingleOrDefault();
                    _lootService.TractorAllLoot(ship);
                    await Clients.Caller.ReceiveMessage("Looted all.");
                }
            }
        }

        /// <summary>
        /// Given a specific ship contained within the req object and owned by the player,
        /// vacuum up all the loot at the given ship's coordinates.
        /// </summary>
        /// <param name="ctr">A container containing authorization information</param>
        /// <param name="req">A request containing the ship id of the ship grabbing loot and the ID of the loot object such as module being looted</param>
        public async Task TakeSpecificLoot(AuthorizationTokenContainer ctr, TakeSpecificLootRequest req)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(ctr.Token);
            if (playerByAccessTokenResponse.Success == true)
            {
                GetShipsByPlayerIdResponse getShipsByPlayerIdResponse = _gameService.GetShipsByPlayerId(playerByAccessTokenResponse.Player.Id);
                if (getShipsByPlayerIdResponse.Success)
                {
                    Ship ship = getShipsByPlayerIdResponse.Ships.Where(x => x.Id == req.ShipId).SingleOrDefault();
                    await Clients.Caller.ReceiveMessage("Looted specific item.");
                }
            }
        }


        public async Task Ping(AuthorizationTokenContainer authorizationTokenCtr, PingRequest pingRequest)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(authorizationTokenCtr.Token);
            var result = new PingResponse();
            result.PingId = pingRequest.PingId;
            result.Error = null;
            result.Success = true;
            await Clients.Caller.ReceivePingResponse(result);
        }

        public async Task GetCharacterForManagement(AuthorizationTokenContainer tokenContainer, CharacterForManagementRequest characterForManagementRequest)
        {
            var result = new GetCharacterForManagementResult();
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetCharacterByPlayerIdAndCharacterIdResponse retrievedCharacter = _gameService.GetCharacterByPlayerIdAndCharacter(playerByAccessTokenResponse.Player.Id, characterForManagementRequest.CharacterId);
                if (retrievedCharacter.Success)
                {
                    result.Success = true;
                    result.Character = retrievedCharacter.Character;
                    result.Error = null;
                    await Clients.Caller.ReceiveCharacterForManagement(result);
                }
            }
        }

        public async Task GetPlayerCameraCoordinates(AuthorizationTokenContainer tokenContainer)
        {
            var result = new GetPlayerCameraCoordinatesResult();
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                result.X = playerByAccessTokenResponse.Player.CameraX;
                result.Y = playerByAccessTokenResponse.Player.CameraY;
                result.Z = playerByAccessTokenResponse.Player.CameraZ;
                await Clients.Caller.ReceivePlayerCameraCoordinates(result);
            }
        }

        public async Task GetMapAtShip(AuthorizationTokenContainer tokenContainer, MapAtShipRequest mapAtShipRequestContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, mapAtShipRequestContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    GetMapAtShipByShipIdResponse map = _mapService.GetMapAtShipByShipId(ship.Id, mapAtShipRequestContainer.ViewWidth, mapAtShipRequestContainer.ViewHeight);
                    if (map.Success)
                    {
                        await Clients.Caller.ReceiveMapData(map.MapDataResult);
                    }
                }
            }
        }

        public async Task UpdateShipPosition(AuthorizationTokenContainer tokenContainer, ShipMovementContainer shipMovementContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, shipMovementContainer.ShipId);
                if (serviceResult.Success)
                {
                    var timeSpan = (DateTime.UtcNow - serviceResult.Ships.First().LastMovementUtc);
                    // if the player is an administrator, don't apply a speed limit to them.
                    if ((playerByAccessTokenResponse.Player.IsAdmin == true) || timeSpan.TotalMilliseconds > 500 && shipMovementContainer.ChangeX < 2 && shipMovementContainer.ChangeY < 2)
                    {
                        _gameService.MoveShipRelative(shipMovementContainer.ShipId, shipMovementContainer.ChangeX, shipMovementContainer.ChangeY);
                        ShipMovementConfirmation confirmation = new ShipMovementConfirmation();
                        confirmation.ConfirmationId = shipMovementContainer.ConfirmationId;
                        await Clients.Caller.ReceiveShipMovementConfirmation(confirmation);
                    }
                    else
                    {
                        /// TODO: display error for exceeding speed limit.
                        await Clients.Caller.ReceiveMessage("Exceeded speed limit.");
                    }
                }
            }
        }

        public async Task AddWarpStart(AuthorizationTokenContainer tokenContainer, SelectedShipContainer selectedShipContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success && playerByAccessTokenResponse.Player.IsAdmin == true)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, selectedShipContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    CreateSpaceObjectResult spaceObjectResult = _objectService.SpawnWarpGate(ship.X, ship.Y, ship.Z);
                    if (spaceObjectResult.Success == true)
                    {
                        await Clients.Caller.ReceiveMessage("Space object #" + spaceObjectResult.SpaceObject.Id + " created.");
                    }
                    else
                    {
                        await Clients.Caller.ReceiveMessage("Space object could not be created.");
                    }
                }
                else
                {
                    await Clients.Caller.ReceiveError(new ErrorFromServer("Could not retrieve the ship you are piloting for object placement or selection purposes."));
                }
            }
            else
            {
                await Clients.Caller.ReceiveError(new ErrorFromServer("Warp start selection is only available to administrators."));
            }
        }

        public async Task AddWarpEnd(AuthorizationTokenContainer tokenContainer, SelectedShipContainer selectedShipContainer, SelectedWarpStartContainer selectedWarpStart)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success && playerByAccessTokenResponse.Player.IsAdmin == true)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, selectedShipContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    ConnectWarpgateResult connectResult = _objectService.ConnectWarpGate(ship.X, ship.Y, ship.Z, selectedWarpStart.WarpStartId);
                    if (connectResult.Success == true)
                    {
                        await Clients.Caller.ReceiveMessage("Connected warpgates together: " + connectResult.SourceObjectId + " --> " + connectResult.DestinationObjectId);
                    }
                    else
                    {
                        await Clients.Caller.ReceiveMessage("Space object could not be created.");
                    }
                }
                else
                {
                    await Clients.Caller.ReceiveError(new ErrorFromServer("Could not retrieve the ship you are piloting for object placement or selection purposes."));
                }
            }
            else
            {
                await Clients.Caller.ReceiveError(new ErrorFromServer("Warp ending spot creation is only available to administrators."));
            }
        }

        public async Task AddShipModule(AuthorizationTokenContainer tokenContainer, SelectedShipContainer selectedShipContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success && playerByAccessTokenResponse.Player.IsAdmin == true)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, selectedShipContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    _lootService.SpawnRandomModule(ship.X, ship.Y, ship.Z);
                    Clients.Caller.ReceiveMessage("Random module spawned!");
                }
                else
                {
                    await Clients.Caller.ReceiveError(new ErrorFromServer("Could not retrieve the ship you are piloting for object placement or selection purposes."));
                }
            }
            else
            {
                await Clients.Caller.ReceiveError(new ErrorFromServer("Loot spawning is is only available to administrators."));
            }
        }

        public async Task ScanShipLocationForLoot(AuthorizationTokenContainer tokenContainer, SelectedShipContainer selectedShipContainer)
        {
            GetPlayerByAccessTokenResponse playerByAccessTokenResponse = _authService.GetPlayerByAccessToken(tokenContainer.Token);
            if (playerByAccessTokenResponse.Success && playerByAccessTokenResponse.Player.IsAdmin == true)
            {
                GetShipsByPlayerIdResponse serviceResult = _gameService.GetShipByPlayerId(playerByAccessTokenResponse.Player.Id, selectedShipContainer.ShipId);
                if (serviceResult.Success)
                {
                    Ship ship = serviceResult.Ships.First();
                    List<SpaceLoot> loot = _lootService.GetAllSpaceLoot(ship.X, ship.Y, ship.Z);
                    LootScanResponse scanResponse = new LootScanResponse();
                    scanResponse.X = ship.X;
                    scanResponse.Y = ship.Y;
                    scanResponse.Z = ship.Z;
                    if (loot.Count > 0)
                    {
                        scanResponse.SpaceLoots = loot;
                    }
                    else
                    {
                        scanResponse.SpaceLoots = null;
                    }
                    Clients.Caller.ReceiveLootScanResponse(scanResponse);
                }
                else
                {
                    await Clients.Caller.ReceiveError(new ErrorFromServer("Could not retrieve the ship you are piloting for object placement or selection purposes."));
                }
            }
            else
            {
                await Clients.Caller.ReceiveError(new ErrorFromServer("Loot spawning is is only available to administrators."));
            }
        }

    }

}
