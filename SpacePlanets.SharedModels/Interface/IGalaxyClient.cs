using SpacePlanets.SharedModels.ServerToClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpacePlanets.SharedModels.Interface
{
    public interface IGalaxyClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveServerTime(string message);

        Task ReceiveAccessTokenResult(GetAccessTokenResult result);

        Task ReceiveCharactersForMenu(GetCharactersForMenuResult result);

        Task ReceivePingResponse(PingResponse result);

        Task ReceiveCharacterForManagement(GetCharacterForManagementResult result);


    }
}
