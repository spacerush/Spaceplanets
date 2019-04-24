using SpLib.DataTransfer.ServerToClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public interface IGalaxyClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveServerTime(string message);
        Task ReceiveCharactersForMenu(GetCharactersForMenuResult result);

    }
}
