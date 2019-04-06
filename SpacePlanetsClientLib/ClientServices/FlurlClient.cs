using System;
using System.Collections.Generic;
using System.Text;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using SpacePlanetsClientLib.Results;
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

        public GetAccessTokenResult GetAccessToken(string username, string password)
        {
            var output = new GetAccessTokenResult();
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
                output.Success = true;
                output.Token = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(result.Content.ReadAsStringAsync().Result);
                output.Error = null;
            }
            else
            {
                output.Success = false;
                string content = result.Content.ReadAsStringAsync().Result;
                output.Error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(content);
                output.Token = null;
            }
            return output;
        }

        public GetAccessTokenResult GetAccessToken(RefreshToken refreshToken)
        {
            var output = new GetAccessTokenResult();
            var dto = new RefreshTokenContainer();
            dto.Content = refreshToken.Content;
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetTokenUsingRefreshToken")
                .PostJsonAsync(dto).Result;
            if (result.IsSuccessStatusCode)
            {
                output.Success = true;
                output.Token = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(result.Content.ReadAsStringAsync().Result);
                output.Error = null;
            }
            else
            {
                output.Success = false;
                output.Error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(result.Content.ReadAsStringAsync().Result);
                output.Token = null;
            }
            return output;
        }

    }
}
