using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class PingResponse
    {
        public DateTime OriginalDateTime { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public bool Success { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
