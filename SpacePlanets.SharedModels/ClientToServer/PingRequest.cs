using SpacePlanets.SharedModels.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class PingRequest
    {
        public string PingId { get; set; }
        public PingRequest()
        {
            PingId = GenerationHelper.CreateRandomString(true, true, false, 7);
        }
    }
}
