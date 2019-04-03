using System;
using System.Collections.Generic;
using System.Text;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;

namespace SpacePlanetsClientLib.ClientServices
{
    public class FlurlClient : IFlurlClient
    {
        private string _endpoint;

        public FlurlClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public void ChangeEndpoint(string newEndpoint)
        {
            _endpoint = newEndpoint;
        }

        public bool GetAccessToken(string username, string password, out AccessToken token, out ErrorFromServer error)
        {
            token = null;
            error = null;
            LoginInformation loginInformation = new LoginInformation(username, password);
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetToken")
                .PostJsonAsync(loginInformation).Result;
            if (result.IsSuccessStatusCode)
            {
                token = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(result.Content.ReadAsStringAsync().Result);
                return true;
            }
            else
            {
                error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(result.Content.ReadAsStringAsync().Result);
                return false;
            }
        }

        public bool GetAccessToken(string refreshToken, out AccessToken token, out ErrorFromServer error)
        {
            token = null;
            error = null;
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetTokenUsingRefreshToken")
                .PostJsonAsync(refreshToken).Result;
            if (result.IsSuccessStatusCode)
            {
                token = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(result.Content.ReadAsStringAsync().Result);
                return true; 
            }
            else
            {
                error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(result.Content.ReadAsStringAsync().Result);
                return false;
            }
        }

    }
}
