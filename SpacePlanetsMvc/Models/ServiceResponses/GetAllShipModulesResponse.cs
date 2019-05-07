using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetAllShipModulesResponse
    {
        public List<ShipModule> ShipModules { get; set; }
        public bool Success { get; set; }
    }
}
