using SpLib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsDAL.ServiceResponses
{
    public class GetCharacterByPlayerIdAndCharacterIdResponse
    {
        public Guid PlayerId { get; set; }
        public Guid CharacterId { get; set; }
        public Character Character { get; set; }
        public bool Success { get; set; }
    }
}
