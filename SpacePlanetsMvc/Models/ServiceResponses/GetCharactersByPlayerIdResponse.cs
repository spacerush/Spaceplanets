using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsMvc.ServiceResponses
{
    public class GetCharactersByPlayerIdResponse
    {
        public Guid PlayerId { get; set; }
        public List<Character> Characters { get; set; }
        public bool Success { get; set; }
    }
}
