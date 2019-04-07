using SpacePlanetsClientLib.Results;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.ClientServices
{
    public interface IFlurlGlancesClient
    {


        /// <summary>
        /// Change the URL to which this client is communicating for API calls.
        /// </summary>
        /// <param name="newEndpoint">The URL of the api server</param>
        void ChangeEndpoint(string newEndpoint);

        GetCpuUseResult GetCpuUse();

        GetMemUseResult GetMemUse();
    }
}
