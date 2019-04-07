using System;
using System.Collections.Generic;
using System.Text;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using SpacePlanetsClientLib.Results;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Glances;
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

        public GetMemUseResult GetMemoryUse()
        {
            var output = new GetMemUseResult();
            try
            {
                var result = _endpoint
                    .AllowAnyHttpStatus()
                    .WithHeader("Accept-Version", "1.0")
                    .AppendPathSegment("api")
                    .AppendPathSegment("Rpc")
                    .AppendPathSegment("GetMemUse")
                    .GetJsonAsync<GetMemUseResult>().Result;
                if (result != null)
                {
                    output = result;
                }
            }
            catch (Exception ex)
            {
                output.Success = false;
                output.MemoryInformation = null;
                output.Error = new ErrorFromServer("Could not retrieve server memory use");
                output.Error.ErrorId = "ClientGenerated";
            }
            return output;
       }

        public GetCpuUseResult GetCpuUse()
        {
            var output = new GetCpuUseResult();
            try
            {
                var result = _endpoint
                    .AllowAnyHttpStatus()
                    .WithHeader("Accept-Version", "1.0")
                    .AppendPathSegment("api")
                    .AppendPathSegment("Rpc")
                    .AppendPathSegment("GetCpuUse")
                    .GetJsonAsync<GetCpuUseResult>().Result;
                if (result != null)
                {
                    output = result;
                }
            }
            catch (Exception ex)
            {
                output.Success = false;
                output.CpuInformation = null;
                output.Error = new ErrorFromServer("Could not retrieve server cpu use");
                output.Error.ErrorId = "ClientGenerated";
            }
            return output;
        }

        public PingResponse GetPingResponse()
        {
            var input = new PingRequest();
            input.DateTime = DateTime.Now;
            var output = new PingResponse();
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("Ping")
                .PostJsonAsync(input).Result;
            return output;
        }

        public GetCharactersForMenuResult GetCharactersForManagementMenu(string authorizationToken)
        {
            List<GenericItemForPicklist> genericItemsForPicklist = new List<GenericItemForPicklist>();
            var input = new AuthorizationTokenContainer();
            input.Content = authorizationToken;
            var output = new GetCharactersForMenuResult();
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetCharactersForMenu")
                .PostJsonAsync(input).Result;
                output = JsonConvert.DeserializeObject<GetCharactersForMenuResult>(result.Content.ReadAsStringAsync().Result);
            return output;
        }

        public GetShipsForMenuResult GetShipsForManagementMenu(string authorizationToken)
        {
            List<GenericItemForPicklist> genericItemsForPicklist = new List<GenericItemForPicklist>();
            var input = new AuthorizationTokenContainer();
            input.Content = authorizationToken;
            var output = new GetShipsForMenuResult();
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetShipsForMenu")
                .PostJsonAsync(input).Result;
            output = JsonConvert.DeserializeObject<GetShipsForMenuResult>(result.Content.ReadAsStringAsync().Result);
            return output;
        }

    }
}
