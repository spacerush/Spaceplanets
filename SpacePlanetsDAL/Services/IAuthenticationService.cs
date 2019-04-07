using SpacePlanetsDAL.ServiceResponses;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.Services
{
    public interface IAuthenticationService
    {

        string HashPassword(string password);
        
        bool VerifyPassword(string password, string storedHash);

        AccessToken CreateGameplayToken(string username);

        bool TryLoginCredentials(string username, string password);

        AccessToken CreateAccessTokenFromRefreshToken(string refreshToken);


        GetPlayerByAccessTokenResponse GetPlayerByAccessToken(string accessToken);


    }
}
