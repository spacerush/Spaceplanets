using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetAllShipTemplatesResponse
    {
        public List<ShipTemplate> ShipTemplates { get; set; }
        public bool Success { get; set; }
    }
}
