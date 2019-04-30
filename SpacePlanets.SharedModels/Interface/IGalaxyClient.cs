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
    }
}
