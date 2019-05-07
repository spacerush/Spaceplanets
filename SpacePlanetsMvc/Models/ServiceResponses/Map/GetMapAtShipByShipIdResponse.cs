using SpacePlanets.SharedModels.ServerToClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePlanetsMvc.Models.ServiceResponses.Map
{
    public class GetMapAtShipByShipIdResponse
    {
        public bool Success { get; set; }
        public GetMapDataResult MapDataResult { get; set; }
    }
}
