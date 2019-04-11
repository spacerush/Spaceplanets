using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePlanetsClient.Extensions;
using SpacePlanetsClient.Consoles;
using SpacePlanetsClientLib.ClientServices;
using SpLib.Objects;
using SpLib.DataTransfer.ServerToClient;
using SpacePlanetsClientLib.Results;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using SpacePlanetsClient.Models;
using System.Collections.Generic;

namespace SpacePlanetsClient
{
    public static class GameState
    {
        internal static CancellationToken CancelHeartbeat;

        public static void SetClient(IFlurlClient client)
        {
            _client = client;
        }

        public static void SetApiEndpoint(string endpoint)
        {
            _client.ChangeEndpoint(endpoint);
        }


        enum GameStatus
        {
            Startup,
            LoggingIn,
            LoggedIn
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
        private static MessageLogConsole _messageLogConsole;
        private static ServerStatusConsole _serverStatusConsole;
        private static MenuBarConsole _menuBarConsole;

        private static AccessToken _accessToken;
        private static IFlurlClient _client;
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
        /// <returns>TRUE if authentication is successful.</returns>
        public static bool DoLogin(string username, string password)
        {
            _gameStatus = GameStatus.LoggingIn;
            GetAccessTokenResult result = _client.GetAccessToken(username, password);
            if (!result.Success)
            {
                _gameStatus = GameStatus.Startup;
                CreateErrorWindow(result.Error, _loginWindow.Children.First());
                return false;
            }
            else
            {
                _gameStatus = GameStatus.LoggedIn;

                _accessToken = result.Token;
                _messageLogConsole = new MessageLogConsole(_mainConsole.Width, _mainConsole.Height / 4);
                _messageLogConsole.Position = new Point(0, _mainConsole.Height - _messageLogConsole.Height);
                _messageLogConsole.IsVisible = true;
                _mainConsole.Children.Add(_messageLogConsole);

                _serverStatusConsole = new ServerStatusConsole(_mainConsole.Width, 1);
                _serverStatusConsole.Position = new Point(0, _mainConsole.Height - _messageLogConsole.Height - 1);
                _serverStatusConsole.IsVisible = true;
                _mainConsole.Children.Add(_serverStatusConsole);

                _menuBarConsole = new MenuBarConsole(_mainConsole.Width, 1);
                _menuBarConsole.Position = new Point(0, 0);
                _menuBarConsole.IsVisible = true;
                _mainConsole.Children.Add(_menuBarConsole);

                GameState.ShipMenu = new MenuConsole(_mainConsole.Width, _mainConsole.Height -1);
                GameState.CharacterMenu = new MenuConsole(_mainConsole.Width, _mainConsole.Height -1);
                ShipMenu.Position = new Point(0, 1);
                CharacterMenu.Position = new Point(0, 1);

                _loginWindow.IsVisible = false;
                _loginWindow.Controls.RemoveAll();
                _loginWindow.Clear();

                StartHeartbeat(TimeSpan.FromSeconds(10), CancelHeartbeat);
                return true;
            }
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _client.GetPingResponse();
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            if (ts.TotalMilliseconds < 500)
            {
                _serverStatusConsole.Write("Ping: " + Math.Floor(ts.TotalMilliseconds), ServerStatusConsole.MessageTypes.Ok);
            }
            else
            {
                _serverStatusConsole.Write("Ping: " + Math.Floor(ts.TotalMilliseconds), ServerStatusConsole.MessageTypes.Danger);
            }
        }

        private static void RefreshTokenIfNeeded()
        {
            // Check for expiration within the next 2 minutes
            if (DateTime.Compare(DateTime.UtcNow.AddMinutes(2), _accessToken.Expiry.ToUniversalTime()) > 0)
            {
                if (_accessToken.RefreshToken.Expiry.ToUniversalTime() > DateTime.UtcNow)
                {
                    _messageLogConsole.Write("Requesting access token refresh using the refresh token.", MessageLogConsole.MessageTypes.AdminOnlyMessage);
                    GetAccessTokenResult getAccessTokenResult = _client.GetAccessToken(_accessToken.RefreshToken);
                    if (getAccessTokenResult.Success)
                    {
                        _accessToken = getAccessTokenResult.Token;
                    }
                    else
                    {
                        CreateErrorWindow(getAccessTokenResult.Error, _mainConsole);
                    }
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

        internal static void RetrieveGalaxyAndDisplay()
        {
            var result = _client.GetGalaxyByName(_accessToken.Content, "Seed 0");
            if (result.Success)
            {
                MainConsole.Print(3, 3, result.GalaxyContainer.Galaxy.Stars.Count() + " stars in galaxy");
            }
        }


        internal static void RetrieveCharactersForCharacterMenu()
        {
            GetCharactersForMenuResult result = _client.GetCharactersForManagementMenu(_accessToken.Content);
            if (result.Success)
            {
                _displayingCharacterMenu = true;
                List<MenuButtonMetadataItem> characters = new List<MenuButtonMetadataItem>();
                foreach (var item in result.Characters)
                {
                    characters.Add(new MenuButtonMetadataItem(Guid.NewGuid(), item.Name, "Character"));
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
                CreateErrorWindow(result.Error, _mainConsole);
            }
        }

        internal static void RetrieveShipsForShipMenu()
        {
            GetShipsForMenuResult result = _client.GetShipsForManagementMenu(_accessToken.Content);
            if (result.Success)
            {
                _displayingShipMenu = true;
                List<MenuButtonMetadataItem> ships = new List<MenuButtonMetadataItem>();
                foreach (var item in result.Ships)
                {
                    ships.Add(new MenuButtonMetadataItem(Guid.NewGuid(), item.Name, "Ship"));
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
        }



        public static void WriteGeneralMessageToLog(string message)
        {
            _messageLogConsole.Write(message, MessageLogConsole.MessageTypes.Status);
        }

    }
}
