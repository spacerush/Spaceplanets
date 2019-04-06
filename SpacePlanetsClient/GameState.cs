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

        private static Console _mainConsole;
        private static LoginWindow _loginWindow;
        private static GameStatus _gameStatus;
        private static MessageLogConsole _messageLogConsole;
        private static AccessToken _accessToken;
        private static IFlurlClient _client;


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
                _accessToken = result.Token;
                _messageLogConsole = new MessageLogConsole(_mainConsole.Width, _mainConsole.Height / 4);
                _messageLogConsole.Position = new Point(0, 0);
                _messageLogConsole.IsVisible = true;
                _mainConsole.Children.Add(_messageLogConsole);
                _gameStatus = GameStatus.LoggedIn;
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
            _messageLogConsole.Write("Heartbeat", true);
        }
        private static void RefreshTokenIfNeeded()
        {
            _messageLogConsole.Write("Token expires in " + TimeSpan.FromTicks(_accessToken.Expiry.Ticks - DateTime.UtcNow.Ticks) + " ticks", true);
            // Check for expiration within the next 2 minutes
            if (DateTime.Compare(DateTime.UtcNow.AddMinutes(2), _accessToken.Expiry.ToUniversalTime()) > 0)
            {
                if (_accessToken.RefreshToken.Expiry.ToUniversalTime() > DateTime.UtcNow)
                {
                    _messageLogConsole.Write("Requesting access token refresh using the refresh token.", true);
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
                    _messageLogConsole.Write("We need a new access token but the refresh token is expired.", true);
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

    }
}
