using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetGalaxyResponse
    {
        public GalaxyContainer GalaxyContainer { get; set; }
        public bool Success { get; set; }
    }
}
