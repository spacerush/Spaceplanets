using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePlanetsClient.Extensions;
using SpacePlanetsClient.Consoles;
using SpacePlanets.SharedModels.GameObjects;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using SpacePlanetsClient.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using System.IO;
using SpacePlanets.SharedModels.ServerToClient;
using SpacePlanets.SharedModels.ClientToServer;
using Microsoft.Extensions.DependencyInjection;
using SpacePlanets.SharedModels.Helpers;

namespace SpacePlanetsClient
{
    public static class GameState
    {
        internal static GetMapDataResult cachedMapData;
        
        private static void SetMapData(GetMapDataResult data)
        {
            cachedMapData = data;
        }

        private static void MoveShipOnClient(Guid shipId, int shipCellX, int shipCellY, int shipCellZ, int changeX, int changeY)
        {
            Ship ship = FindShipInMapById(shipId);
            ship.X = ship.X + changeX;
            ship.Y = ship.Y + changeY;

            List<MapDataCell> newCells = new List<MapDataCell>();

            int newX = 0;
            int newY = 0;
            int newZ = 0;
            foreach (MapDataCell item in cachedMapData.MapDataCells)
            {
                if (item != FindCellInMapContainingShip(shipId))
                {
                    newCells.Add(item);
                }
                else
                {
                    MapDataCell cellWithoutShip = new MapDataCell();
                    cellWithoutShip.CellX = item.CellX;
                    cellWithoutShip.CellY = item.CellY;
                    cellWithoutShip.CellZ = item.CellZ;
                    cellWithoutShip.Stars = item.Stars;
                    cellWithoutShip.SpaceObjects = item.SpaceObjects;
                    cellWithoutShip.Ships = new List<Ship>();
                    cellWithoutShip.Ships.AddRange(item.Ships.Where(x => x.Id != shipId));
                    newCells.Add(cellWithoutShip);
                }
            }
            MapDataCell newLocation = newCells.Where(l => (l.CellX == shipCellX) && (l.CellY == shipCellY) && (l.CellZ == shipCellZ)).SingleOrDefault();
            if (newLocation == null)
            {
                MapDataCell cellWithoutShip = new MapDataCell();
                cellWithoutShip.CellX = shipCellX;
                cellWithoutShip.CellY = shipCellY;
                cellWithoutShip.CellZ = shipCellZ;
                cellWithoutShip.SpaceObjects = new List<SpaceObject>();
                cellWithoutShip.Ships = new List<Ship>();
                cellWithoutShip.Stars = new List<Star>();
                cellWithoutShip.Ships.Add(ship);
                newCells.Add(cellWithoutShip);
            }
            else
            {
                newLocation.Ships.Add(ship);
            }
            cachedMapData.MapDataCells = newCells;
            if (shipCellX <= 0 || shipCellX >= _spaceMap.Width || shipCellY <= 0 || shipCellY >= _spaceMap.Height)
            {
                DownloadMapAtShip(ship.Id);
            }
        }

        private static Ship FindShipInMapById(Guid shipId)
        {
            Ship foundShip = null;
            foreach (MapDataCell item in cachedMapData.MapDataCells.Where(x => x.Ships.Count > 0))
            {
                foundShip = item.Ships.Where(s => s.Id == shipId).FirstOrDefault();
                if (foundShip != null)
                {
                    break;
                }
            }
            return foundShip;
        }

        private static MapDataCell FindCellInMapContainingShip(Guid shipId)
        {
            Ship foundShip = null;
            MapDataCell foundCell = null;
            foreach (MapDataCell item in cachedMapData.MapDataCells.Where(x => x.Ships.Count > 0))
            {
                foundShip = item.Ships.Where(s => s.Id == shipId).FirstOrDefault();
                if (foundShip != null)
                {
                    foundCell = item;
                    break;
                }
            }
            return foundCell;
        }

        public static void MoveSelectedShipUp()
        {
            if (_gameStatus == GameStatus.LoggedIn)
            {
                UpdateServerWithShipPosition(0, -1);

            }
        }

        public static void MoveSelectedShipDown()
        {
            if (_gameStatus == GameStatus.LoggedIn)
            {
                UpdateServerWithShipPosition(0, 1);

            }
        }

        public static void MoveSelectedShipLeft()
        {
            if (_gameStatus == GameStatus.LoggedIn)
            {
                UpdateServerWithShipPosition(-1, 0);
            }
        }

        public static void MoveSelectedShipRight()
        {
            if (_gameStatus == GameStatus.LoggedIn)
            {
                UpdateServerWithShipPosition(1, 0);
            }
        }

        public static void UpdateServerWithShipPosition(int changeX, int changeY)
        {
            if (_gameStatus == GameStatus.LoggedIn)
            {
                _shipMovementStatus = ShipMovementStatus.NotReady;
                connection.InvokeAsync("UpdateShipPosition", GetAuthorizationTokenContainer(), new ShipMovementContainer() { ShipId = selectedShip, ChangeX = changeX, ChangeY = changeY,  ConfirmationId = GenerationHelper.CreateRandomString(true, true, false, 10) });
            }
        }

        private static void DrawMapData()
        {
            _spaceMap.Clear();
            foreach (MapDataCell item in cachedMapData.MapDataCells)
            {
                if (item.Stars != null && item.Stars.Count > 0)
                {
                    foreach (Star star in item.Stars)
                    {
                        _spaceMap.Print(item.CellX, item.CellY, "*", Color.OrangeRed, Color.Black);
                    }
                }
                if (item.Ships != null && item.Ships.Count > 0)
                {
                    foreach (Ship ship in item.Ships)
                    {
                        _spaceMap.Print(item.CellX, item.CellY, "+", Color.Turquoise, Color.Black);
                    }

                }
                if (item.SpaceObjects != null && item.SpaceObjects.Count > 0)
                {
                    foreach (SpaceObject spaceObject in item.SpaceObjects)
                    {
                        if (spaceObject.ObjectType == "Asteroid")
                        {
                            _spaceMap.Print(item.CellX, item.CellY, "`", Color.DimGray, Color.Black);
                        }
                        if (spaceObject.ObjectType == "Moon")
                        {
                            _spaceMap.Print(item.CellX, item.CellY, "o", Color.WhiteSmoke, Color.Black);
                        }
                        if (spaceObject.ObjectType == "Planet")
                        {
                            _spaceMap.Print(item.CellX, item.CellY, "O", Color.CornflowerBlue, Color.Black);
                        }

                    }
                }
            }
        }
        internal static Guid selectedShip;

        internal static CancellationToken CancelHeartbeat;
        internal static HubConnection connection;
        public static HubConnection Connection
        {
            get
            {
                return connection;
            }
        }
        internal static string serverUri;
        internal static Stopwatch pingStopWatch = new Stopwatch();
        internal static string lastPingId;

        public static void SetApiEndpoint(string endpoint)
        {
            serverUri = endpoint;
            connection = new HubConnectionBuilder()
                .WithUrl(serverUri + "GalaxyHub")
                .AddMessagePackProtocol()
                .Build();
                connection.StartAsync();
            BindEventHandlersForconnection();
        }

        enum ShipMovementStatus
        {
            NotReady,
            Ready
        }

        enum GameStatus
        {
            Startup,
            LoggingIn,
            LoggedIn,
            Animating
        }

        internal static SadConsole.Effects.Fade DefaultFade
        {
            get
            {
                return new SadConsole.Effects.Fade()
                {
                    FadeForeground = true,
                    FadeBackground = false,
                    UseCellBackground = true,
                    UseCellForeground = false,
                    DestinationForeground = new ColorGradient(Color.CornflowerBlue, Color.White),
                    FadeDuration = 0.5d,
                    CloneOnApply = false,
                    AutoReverse = true,
                    Repeat = true,
                };
            }
        }

        private static Console _mainConsole;
        public static Console MainConsole
        {
            get
            {
                return _mainConsole;
            }
        }

        private static LoginWindow _loginWindow;
        private static GameStatus _gameStatus;
        private static ShipMovementStatus _shipMovementStatus;
        private static MessageLogConsole _messageLogConsole;
        private static ServerStatusConsole _serverStatusConsole;
        private static MenuBarConsole _menuBarConsole;
        private static SpaceMapConsole _spaceMap;

        private static AccessToken _accessToken;

        public static bool DisplayingCharacterMenu
        {
            get
            {
                return _displayingCharacterMenu;
            }
            set
            {
                _displayingCharacterMenu = DisplayingCharacterMenu;
            }
        }

        public static bool DisplayingShipMenu
        {
            get
            {
                return _displayingShipMenu;
            }
            set
            {
                _displayingShipMenu = DisplayingShipMenu;
            }
        }

        /// <summary>
        /// Whether or not the character selection menu is being displayed.
        /// </summary>
        private static bool _displayingCharacterMenu = false;

        /// <summary>
        /// Whether or not the ship selection menu is being displayed.
        /// </summary>
        private static bool _displayingShipMenu = false;

        public static MenuConsole CharacterMenu { get; set; }
        public static MenuConsole ShipMenu { get; set; }

        /// <summary>
        /// This is the first method in this static class that will be called by Initiation
        /// </summary>
        /// <param name="console">The console with which to use as the root of all things</param>
        public static void InitializeConsole(Console console)
        {
            _gameStatus = GameStatus.Startup;
            _mainConsole = console;
            _loginWindow = new LoginWindow(60, 14);
            _loginWindow.CanDrag = true;
            _loginWindow.TitleAlignment = HorizontalAlignment.Center;
            _loginWindow.Title = "Please Login";
            _loginWindow.IsVisible = true;
            _loginWindow.UseKeyboard = true;
            _mainConsole.Children.Add(_loginWindow);
            _loginWindow.CenterWithinParent();

        }

        public static void SetMenusHidden()
        {
            RemoveCharacterMenu();
            RemoveShipMenu();
        }

        public static void RemoveCharacterMenu()
        {
            _mainConsole.Children.Remove(CharacterMenu);
            GameState.CharacterMenu.IsVisible = false;
            _displayingCharacterMenu = false;
        }

        public static void RemoveShipMenu()
        {
            _mainConsole.Children.Remove(ShipMenu);
            GameState.ShipMenu.IsVisible = false;
            _displayingShipMenu = false;
        }

        /// <summary>
        /// Attempt to log in to the game server (could be lan or other)
        /// </summary>
        /// <param name="username">An account name</param>
        /// <param name="password">A password which should be transmitted using TLS. The server will receive the password and calculate its hash.</param>
        public static void DoLogin(string username, string password)
        {
            var ctr = new CredentialsContainer(username, password);
            _gameStatus = GameStatus.LoggingIn;
            connection.SendAsync("GetAccessToken", ctr);
        }

        private static async Task StartHeartbeat(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                Heartbeat();
                RefreshTokenIfNeeded();
                await Task.Delay(interval, cancellationToken);
            }
        }

        private static void Heartbeat()
        {
            pingStopWatch.Reset();
            pingStopWatch.Start();
            var pingRequest = new PingRequest();
            lastPingId = pingRequest.PingId;
            connection.SendAsync("Ping", new AuthorizationTokenContainer() { Token = _accessToken.Content }, pingRequest);
        }

        private static void RefreshTokenIfNeeded()
        {
            // Check for expiration within the next 2 minutes
            if (DateTime.Compare(DateTime.UtcNow.AddMinutes(2), _accessToken.Expiry.ToUniversalTime()) > 0)
            {
                if (_accessToken.RefreshToken.Expiry.ToUniversalTime() > DateTime.UtcNow)
                {
                    _messageLogConsole.Write("Requesting access token refresh using the refresh token.", MessageLogConsole.MessageTypes.AdminOnlyMessage);
                    connection.InvokeAsync("GetAccessTokenWithRefreshToken", _accessToken.RefreshToken);
                }
                else
                {
                    _messageLogConsole.Write("We need a new access token but the refresh token is expired.", MessageLogConsole.MessageTypes.AdminOnlyMessage);
                }
            }
        }

        private static void CreateErrorWindow(ErrorFromServer errorFromServer, Console windowToReturnFocusTo)
        {
            ErrorWindow errorWindow = new ErrorWindow(60, 14, errorFromServer.Message, errorFromServer.ErrorId.ToString(), windowToReturnFocusTo);
            errorWindow.TitleAlignment = HorizontalAlignment.Center;
            errorWindow.Title = "Server Error";
            errorWindow.CanDrag = true;
            errorWindow.IsVisible = true;
            errorWindow.UseKeyboard = true;
            errorWindow.CenterWithinParent();
        }

        private static void CreateCharacterWindow(Character character)
        {
            var characterWindow = new CharacterManagementWindow(MainConsole.Width / 2, MainConsole.Height / 2, MainConsole);
            MainConsole.Children.Add(characterWindow);
            characterWindow.TitleAlignment = HorizontalAlignment.Center;
            characterWindow.Title = character.Name + ", the level " + character.Level.ToString() + " " + character.Profession;
            characterWindow.CanDrag = true;
            characterWindow.IsVisible = true;
            characterWindow.UseKeyboard = true;
            characterWindow.CenterWithinParent();
        }

        internal static void RetrieveGalaxyAndDisplay()
        {

        }


        internal static void RetrieveCharactersForCharacterMenu()
        {
            connection.InvokeAsync("GetCharactersForMenu", GetAuthorizationTokenContainer());

        }

        internal static void DownloadCharacterForManagement(Guid characterId)
        {
            var request = new CharacterForManagementRequest(characterId);
            connection.InvokeAsync("GetCharacterForManagement", GetAuthorizationTokenContainer(), request);
        }

        internal static void SetSelectedShip(Guid shipId)
        {
            selectedShip = shipId;
            _shipMovementStatus = ShipMovementStatus.Ready;
        }

        internal static void UnselectShip()
        {
            selectedShip = Guid.Empty;
        }

        internal static void DownloadMapAtShip(Guid shipId)
        {
            var request = new MapAtShipRequest();
            request.ShipId = shipId;
            request.ViewWidth = _spaceMap.Width;
            request.ViewHeight = _spaceMap.Height;
            connection.InvokeAsync("GetMapAtShip", GetAuthorizationTokenContainer(), request);
        }

        internal static void RetrieveShipsForShipMenu()
        {
            connection.InvokeAsync("GetShipsForMenu", GetAuthorizationTokenContainer());
        }

        public static void WriteGeneralMessageToLog(string message)
        {
            _messageLogConsole.Write(message, MessageLogConsole.MessageTypes.Status);
        }

        /// <summary>
        /// Takes a list of characters that have been encapsulated into a GetCharactersForMenuResult object and
        /// pops open a menu from which they can be chosen to interact with.
        /// </summary>
        /// <param name="characterresult">The result of a call to the server for data.</param>
        private static void ProcessCharactersForMenu(GetCharactersForMenuResult characterresult)
        {
            if (characterresult.Success)
            {
                _displayingCharacterMenu = true;
                List<MenuButtonMetadataItem> characters = new List<MenuButtonMetadataItem>();
                foreach (var item in characterresult.Characters)
                {
                    characters.Add(new MenuButtonMetadataItem(item.Id, item.Name, "Character"));
                }
                CharacterMenu.SetElements(characters);
                _mainConsole.Children.Add(CharacterMenu);
                _mainConsole.Children.MoveToTop(CharacterMenu);
                string menuTitle = "Manage your character(s)";
                CharacterMenu.ShowMenu(menuTitle);
                int cellX = 0;
                while (cellX < menuTitle.Length)
                {
                    cellX++;
                    CharacterMenu.SetEffect(cellX, 0, CharacterMenu.menuFade);
                }
            }
            else
            {
                CreateErrorWindow(characterresult.Error, _mainConsole);
            }
        }

        /// <summary>
        /// Takes a list of ships that have been encapsulated into a GetShipsForMenuResult object and
        /// pops open a menu from which they can be chosen to interact with.
        /// </summary>
        /// <param name="shipresult">The result of a call to the server for data.</param>
        private static void ProcessShipsForMenu(GetShipsForMenuResult shipresult)
        {
            if (shipresult.Success)
            {
                _displayingShipMenu = true;
                List<MenuButtonMetadataItem> ships = new List<MenuButtonMetadataItem>();
                foreach (var item in shipresult.Ships)
                {
                    ships.Add(new MenuButtonMetadataItem(item.Id, item.Name, "Ship"));
                }
                ShipMenu.SetElements(ships);
                _mainConsole.Children.Add(ShipMenu);
                _mainConsole.Children.MoveToTop(ShipMenu);
                string menuTitle = "Manage your ship(s)";
                ShipMenu.ShowMenu(menuTitle);
                int cellX = 0;
                while (cellX < menuTitle.Length)
                {
                    cellX++;
                    ShipMenu.SetEffect(cellX, 0, ShipMenu.menuFade);
                }
            }
            else
            {
                CreateErrorWindow(shipresult.Error, _mainConsole);
            }
        }

        private static void RequestCameraCoordinates()
        {
            connection.SendAsync("GetPlayerCameraCoordinates", new AuthorizationTokenContainer() { Token = _accessToken.Content });
        }

        private static AuthorizationTokenContainer GetAuthorizationTokenContainer()
        {
            var ctr = new AuthorizationTokenContainer();
            ctr.Token = _accessToken.Content;
            return ctr;
        }

        private static void BindEventHandlersForconnection()
        {
            connection.On<PingResponse>("ReceivePingResponse", (ping) =>
            {
                if (ping.PingId == lastPingId)
                {
                    pingStopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = pingStopWatch.Elapsed;
                    if (ts.TotalMilliseconds < 100)
                    {
                        _serverStatusConsole.Write("Scanner responsiveness: " + Math.Floor(ts.TotalMilliseconds) + " ms", ServerStatusConsole.MessageTypes.Ok);
                    }
                    else
                    {
                        _serverStatusConsole.Write("Scanner responsiveness degraded: " + Math.Floor(ts.TotalMilliseconds) + " ms", ServerStatusConsole.MessageTypes.Danger);
                        _messageLogConsole.Write("Scanner responsiveness degraded: " + Math.Floor(ts.TotalMilliseconds) + " ms", MessageLogConsole.MessageTypes.Warning);
                    }
                }
                else
                {
                    pingStopWatch.Stop();
                    _serverStatusConsole.Write("Scanner responsiveness probe error (data received out of order).", ServerStatusConsole.MessageTypes.Danger);
                    _messageLogConsole.Write("Scanner responsiveness probe error (data received out of order).", MessageLogConsole.MessageTypes.Warning);
                }
            });

            connection.On<string>("ReceiveMessage", (message) =>
            {
                if (_gameStatus == GameStatus.LoggedIn)
                {
                    _messageLogConsole.Write("Receive Chat: " + message);
                }
            });

            connection.On<string>("ReceiveServerTime", (serverTime) =>
            {
                if (_gameStatus == GameStatus.LoggedIn)
                {
                    // TODO: REMOVE THIS
                    //_messageLogConsole.Write("Receive server time: " + serverTime);
                }
            });

            connection.On<GetAccessTokenResult>("ReceiveAccessTokenResult", (result) =>
            {
                if (!result.Success)
                {
                    _shipMovementStatus = ShipMovementStatus.NotReady;
                    _gameStatus = GameStatus.Startup;
                    CreateErrorWindow(result.Error, _loginWindow.Children.First());
                }
                else
                {
                    _shipMovementStatus = ShipMovementStatus.NotReady;
                    _gameStatus = GameStatus.LoggedIn;
                    _accessToken = result.Token;
                    RequestCameraCoordinates();

                    _messageLogConsole = new MessageLogConsole(_mainConsole.Width, _mainConsole.Height / 4);
                    _messageLogConsole.Position = new Point(0, _mainConsole.Height - _messageLogConsole.Height);
                    _messageLogConsole.IsVisible = true;
                    _mainConsole.Children.Add(_messageLogConsole);

                    _serverStatusConsole = new ServerStatusConsole(_mainConsole.Width, 1);
                    _serverStatusConsole.Position = new Point(0, _mainConsole.Height - _messageLogConsole.Height - 1);
                    _serverStatusConsole.IsVisible = true;
                    _mainConsole.Children.Add(_serverStatusConsole);

                    _spaceMap = new SpaceMapConsole(_mainConsole.Width, _mainConsole.Height - _messageLogConsole.Height - 2);
                    _spaceMap.Position = new Point(0, 1);
                    _spaceMap.IsVisible = true;
                    _mainConsole.Children.Add(_spaceMap);

                    _menuBarConsole = new MenuBarConsole(_mainConsole.Width, 1);
                    _menuBarConsole.Position = new Point(0, 0);
                    _menuBarConsole.IsVisible = true;
                    _mainConsole.Children.Add(_menuBarConsole);

                    GameState.ShipMenu = new MenuConsole(_mainConsole.Width, _mainConsole.Height - 1);
                    GameState.CharacterMenu = new MenuConsole(_mainConsole.Width, _mainConsole.Height - 1);
                    ShipMenu.Position = new Point(0, 1);
                    CharacterMenu.Position = new Point(0, 1);

                    _loginWindow.IsVisible = false;
                    _loginWindow.Controls.RemoveAll();
                    _loginWindow.Clear();
                    StartHeartbeat(TimeSpan.FromSeconds(10), CancelHeartbeat);
                }
            });

            connection.On<GetAccessTokenResult>("ReceiveAccessTokenFromRefreshToken", (result) =>
            {
                if (result.Success)
                {
                    _accessToken = result.Token;
                }
            });

            connection.On<GetShipsForMenuResult>("ReceiveShipsForMenu", (result) =>
            {
                if (result.Success)
                {
                    _displayingShipMenu = true;
                    List<MenuButtonMetadataItem> ships = new List<MenuButtonMetadataItem>();
                    foreach (var item in result.Ships)
                    {
                        ships.Add(new MenuButtonMetadataItem(item.Id, item.Name, "Ship"));
                    }
                    ShipMenu.SetElements(ships);
                    _mainConsole.Children.Add(ShipMenu);
                    _mainConsole.Children.MoveToTop(ShipMenu);
                    string menuTitle = "Manage your ship(s)";
                    ShipMenu.ShowMenu(menuTitle);
                    int cellX = 0;
                    while (cellX < menuTitle.Length)
                    {
                        cellX++;
                        ShipMenu.SetEffect(cellX, 0, ShipMenu.menuFade);
                    }
                }
                else
                {
                    CreateErrorWindow(result.Error, _mainConsole);
                }
            });

            // retrieval of characters for menu.
            connection.On<GetCharactersForMenuResult>("ReceiveCharactersForMenu", (param) =>
            {
                ProcessCharactersForMenu(param);
            });

            connection.On<GetCharacterForManagementResult>("ReceiveCharacterForManagement", (param) =>
            {
                CreateCharacterWindow(param.Character);
            });

            connection.On<GetPlayerCameraCoordinatesResult>("ReceivePlayerCameraCoordinates", (param) =>
            {
                _messageLogConsole.Write("Camera coordinates received: " + param.X + "," + param.Y + "," + param.Z, MessageLogConsole.MessageTypes.Status);
            });

            connection.On<GetMapDataResult>("ReceiveMapData", (param) =>
            {
                _messageLogConsole.Write("Receive map data.");
                SetMapData(param);
                DrawMapData();
            });

            connection.On<ShipMovementConfirmation>("ReceiveShipMovementConfirmation", (param) =>
            {
                _shipMovementStatus = ShipMovementStatus.Ready;
                DownloadMapAtShip(selectedShip);
            });

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
    }
}
