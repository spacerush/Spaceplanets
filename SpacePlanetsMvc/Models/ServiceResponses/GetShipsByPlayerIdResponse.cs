using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetShipsByPlayerIdResponse
    {
        public Guid PlayerId { get; set; }
        public List<Ship> Ships { get; set; }
        public bool Success { get; set; }
    }
}
