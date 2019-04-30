using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetCharacterByPlayerIdAndCharacterIdResponse
    {
        public Guid PlayerId { get; set; }
        public Guid CharacterId { get; set; }
        public Character Character { get; set; }
        public bool Success { get; set; }
    }
}
