using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class SaveGalaxyResponse
    {
        public Guid GalaxyContainerId { get; set; }
        public bool Success { get; set; }
    }
}
