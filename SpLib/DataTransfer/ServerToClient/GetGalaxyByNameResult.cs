using SpLib.DataTransfer.ServerToClient;
using SpLib.Objects;
using SpLib.Shared;
using System.Collections.Generic;

namespace SpLib.DataTransfer.ServerToClient
{
    public class GetGalaxyByNameResult
    {
        public bool Success { get; set; }
        public GalaxyContainer GalaxyContainer { get; set; }
    }
}
