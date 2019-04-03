using MongoDB.Driver;
using SpLib.Helpers;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SpacePlanetsDAL.Services
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

        public AccessToken CreateGameplayToken(string username)
        {
            AccessToken accessToken = new AccessToken();
            accessToken.Content = GenerationHelper.CreateRandomString(true, true, false, 20);
            accessToken.Expiry = DateTime.UtcNow.AddMinutes(60);
            accessToken.RefreshToken = new RefreshToken();
            accessToken.RefreshToken.Content = GenerationHelper.CreateRandomString(true, true, false, 20);
            accessToken.RefreshToken.Expiry = DateTime.UtcNow.AddMinutes(90);
            Player player = _wrapper.PlayerRepository.GetOne<Player>(f => f.Username == username);
            player.AccessTokens.Add(accessToken);
            _wrapper.PlayerRepository.UpdateOne<Player>(player);
            return accessToken;
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
            }
            if (VerifyPassword(password, player.PasswordHash)) {
                return true;
            }
            return false;

        }

    }
}
