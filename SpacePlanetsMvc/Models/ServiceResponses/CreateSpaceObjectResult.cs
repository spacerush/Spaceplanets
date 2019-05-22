using SpacePlanets.SharedModels.GameObjects;
using SpacePlanets.SharedModels.ServerToClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.ServiceResponses
{
    public class CreateSpaceObjectResult
    {
        public SpaceObject SpaceObject { get; set; }
        public bool Success { get; set; }
    }
}
