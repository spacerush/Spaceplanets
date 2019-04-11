using SpacePlanetsClientLib.Results;
using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClientLib.ClientServices
{
    public interface IFlurlClient
    {

        GetAccessTokenResult GetAccessToken(string username, string password);

        /// <summary>
        /// Change the URL to which this client is communicating for API calls.
        /// </summary>
        /// <param name="newEndpoint">The URL of the api server</param>
        void ChangeEndpoint(string newEndpoint);

        GetGalaxyByNameResult GetGalaxyByName(string authorizationToken, string galaxyName);

        GetAccessTokenResult GetAccessToken(RefreshToken refreshToken);

        GetCpuUseResult GetCpuUse();
        GetMemUseResult GetMemoryUse();

        PingResponse GetPingResponse();

        GetCharactersForMenuResult GetCharactersForManagementMenu(string authorizationToken);

        GetShipsForMenuResult GetShipsForManagementMenu(string authorizationToken);
    }
}
