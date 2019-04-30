using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetGalaxyResponse
    {
        public GalaxyContainer GalaxyContainer { get; set; }
        public bool Success { get; set; }
    }
}
