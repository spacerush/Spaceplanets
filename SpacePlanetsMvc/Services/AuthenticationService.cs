using MongoDB.Driver;
using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.Helpers;
using SpacePlanetsMvc.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly Repositories.IRepositoryWrapper _wrapper;
        private readonly IMongoClient _mongoClient;
        private readonly Random _random;

        public AuthenticationService(IMongoClient client)
        {
            _mongoClient = client;
            _wrapper = new Repositories.RepositoryWrapper(_mongoClient);
            _random = new Random();
        }

        public string HashPassword(string password)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32);
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            return rfc2898DeriveBytes.IterationCount + "|" + Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            // Generate the hash, with an automatic 32 byte salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(storedHash.Split('|')[1]));
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            //Return the salt and the hash
            if (Convert.ToBase64String(hash) == storedHash.Split('|')[2])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a new access token for a given user with a default lifetime.
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <returns>Resulting AccessToken</returns>
        public AccessToken CreateGameplayToken(string username)
        {
            AccessToken accessToken = new AccessToken();
            accessToken.Content = GenerationHelper.CreateRandomString(true, true, false, 20);
            accessToken.Expiry = DateTime.UtcNow.AddMinutes(5);
            accessToken.RefreshToken = new RefreshToken();
            accessToken.RefreshToken.Content = GenerationHelper.CreateRandomString(true, true, false, 20);
            accessToken.RefreshToken.Expiry = DateTime.UtcNow.AddMinutes(10);
            Player player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Username == username);
            accessToken.PlayerId = player.Id;
            _wrapper.AccessTokenRepository.AddOne<AccessToken>(accessToken);
            return accessToken;
        }

        /// <summary>
        /// Creates a new web session and return it for a given user.
        /// </summary>
        /// <param name="username">The username for which the session is for.</param>
        /// <returns>A WebSession</returns>
        public WebSession CreateWebSession(string username)
        {
            Player player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Username == username);
            WebSession webSession = new WebSession();
            webSession.Expiry = DateTime.UtcNow.AddDays(14);
            webSession.UserId = player.Id;
            webSession.ForUsername = username;
            webSession.SessionCookie = GenerationHelper.CreateRandomString(true, true, false, 32);
            _wrapper.WebSessionRepository.AddOne<WebSession>(webSession);
            return webSession;
        }

        public bool TryLoginCredentials(string username, string password)
        {
            Player player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Username == username);
            if (player == null)
            {
                Player newPlayer = new Player();
                newPlayer.PasswordHash = HashPassword(password);
                newPlayer.Username = username;
                _wrapper.PlayerRepository.AddOne<Player>(newPlayer);
                player = newPlayer;
                Character character = new Character("Nameless Explorer", 1, "Explorer");
                character.PlayerId = player.Id;
                _wrapper.CharacterRepository.AddOne<Character>(character);
            }
            if (VerifyPassword(password, player.PasswordHash))
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Generate a new access token using a longer-lived token issued to clients. Expires the refresh token used.
        /// </summary>
        /// <param name="refreshToken">The refresh token to use to locate the access token in the database.</param>
        public AccessToken CreateAccessTokenFromRefreshToken(string refreshToken)
        {
            AccessToken token = _wrapper.AccessTokenRepository.GetOne<AccessToken>(f => f.RefreshToken.Content == refreshToken && f.RefreshToken.Used == false);
            if (token != null)
            {
                if (token.Expiry > DateTime.UtcNow)
                {
                    Player player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Id == token.PlayerId);
                    token.RefreshToken.Used = true;
                    _wrapper.AccessTokenRepository.UpdateOne<AccessToken>(token);
                    return CreateGameplayToken(player.Username);
                }
            }
            return null;
        }

        /// <summary>
        /// Check the database for something in the access token collection that is valid, then retrieve the matching player.
        /// </summary>
        /// <param name="accessToken">An access token usually issued to players on logging in.</param>
        public GetPlayerByAccessTokenResponse GetPlayerByAccessToken(string accessToken)
        {
            GetPlayerByAccessTokenResponse result = new GetPlayerByAccessTokenResponse();
            AccessToken token = _wrapper.AccessTokenRepository.GetOne<AccessToken>(f => f.Content == accessToken);
            if (token.Expiry > DateTime.UtcNow)
            {
                result.Player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Id == token.PlayerId);
                result.Success = true;
            }
            else
            {
                result.Player = new Player();
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Check the database for something in the cookie collection that is valid, then retrieve the matching player.
        /// </summary>
        /// <param name="cookie">A long-lived session token usually issued to players on logging on the website.</param>
        public GetPlayerByCookieResponse GetPlayerByWebCookie(string cookie)
        {
            GetPlayerByCookieResponse result = new GetPlayerByCookieResponse();
            WebSession webSession = _wrapper.WebSessionRepository.GetOne<WebSession>(f => f.SessionCookie == cookie);
            if (webSession != null && webSession.Expiry > DateTime.UtcNow)
            {
                result.Player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Id == webSession.UserId);
                result.Success = true;
            }
            else
            {
                result.Player = new Player();
                result.Success = false;
            }
            return result;
        }



    }
}
