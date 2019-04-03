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

namespace SpacePlanetsClient
{
    public static class GameState
    {
        private static IFlurlClient _client;

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
            LoggedIn,
            DisplayingMap
        }

        private static Console _mainConsole;
        private static LoginWindow _loginWindow;
        private static GameStatus _gameStatus;

        private static AccessToken _accessToken;

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
            bool result = _client.GetAccessToken(username, password, out AccessToken accessToken, out ErrorFromServer error);
            if (!result)
            {
                _gameStatus = GameStatus.Startup;
                ErrorWindow errorWindow = new ErrorWindow(60, 14, error.Message, error.ErrorId.ToString());
                errorWindow.TitleAlignment = HorizontalAlignment.Center;
                errorWindow.Title = "Server Error";
                errorWindow.CanDrag = true;
                errorWindow.IsVisible = true;
                errorWindow.UseKeyboard = true;
                errorWindow.CenterWithinParent();                
                return false;
            }
            else
            {
                _accessToken = accessToken;
                _gameStatus = GameStatus.LoggedIn;
                _loginWindow.IsVisible = false;
                _loginWindow.Controls.RemoveAll();
                _loginWindow.Clear();
                return true;
            }
        }


    }
}
