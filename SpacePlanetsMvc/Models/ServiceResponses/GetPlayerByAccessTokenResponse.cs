using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetPlayerByAccessTokenResponse
    {
        public Player Player { get; set; }
        public bool Success { get; set; }
    }
}
