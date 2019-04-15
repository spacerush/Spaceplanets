using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetAllShipModulesResponse
    {
        public List<ShipModule> ShipModules { get; set; }
        public bool Success { get; set; }
    }
}
