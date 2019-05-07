using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class PingResponse
    {
        public string PingId { get; set; }
        public bool Success { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
