using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetCharactersByPlayerIdResponse
    {
        public Guid PlayerId { get; set; }
        public List<Character> Characters { get; set; }
        public bool Success { get; set; }
    }
}
