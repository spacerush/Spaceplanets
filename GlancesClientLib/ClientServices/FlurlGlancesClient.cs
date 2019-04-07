using System;
using System.Collections.Generic;
using System.Text;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using SpacePlanetsClientLib.Results;
using SpLib.DataTransfer.ClientToServer;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Glances;
using SpLib.Objects;

namespace SpacePlanetsClientLib.ClientServices
{
    public class FlurlGlancesClient : IFlurlGlancesClient
    {
        private string _endpoint;

        public FlurlGlancesClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public void ChangeEndpoint(string newEndpoint)
        {
            _endpoint = newEndpoint;
        }


        public GetCpuUseResult GetCpuUse()
        {
            var output = new GetCpuUseResult();
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetCpuUse")
                .GetJsonAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                output = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCpuUseResult>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                output.Success = false;
                output.Error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(result.Content.ReadAsStringAsync().Result);
                output.CpuInformation = null;
            }
            return output;
        }

        public GetMemUseResult GetMemUse()
        {
            var output = new GetMemUseResult();
            var result = _endpoint
                .AllowAnyHttpStatus()
                .WithHeader("Accept-Version", "1.0")
                .AppendPathSegment("api")
                .AppendPathSegment("Rpc")
                .AppendPathSegment("GetMemoryUse").GetJsonAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                output = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMemUseResult>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                output.Success = false;
                output.Error = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorFromServer>(result.Content.ReadAsStringAsync().Result);
                output.MemoryInformation = null;
            }
            return output;
        }

    }
}
