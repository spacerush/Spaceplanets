using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class GetCharactersForMenuResult
    {
        public bool Success { get; set; }
        public List<GenericItemForPicklist> Characters { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
