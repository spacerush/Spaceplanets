using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class GetGalaxyByNameResult
    {
        public bool Success { get; set; }
        public GalaxyContainer GalaxyContainer { get; set; }
    }
}
