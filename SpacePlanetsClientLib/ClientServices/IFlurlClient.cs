using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.ClientServices
{
    public interface IFlurlClient
    {

        bool GetAccessToken(string username, string password, out AccessToken token, out ErrorFromServer error);


        bool GetAccessToken(string refreshToken, out AccessToken token, out ErrorFromServer error);

        /// <summary>
        /// Change the URL to which this client is communicating for API calls.
        /// </summary>
        /// <param name="newEndpoint">The URL of the api server</param>
        void ChangeEndpoint(string newEndpoint);
    }
}
