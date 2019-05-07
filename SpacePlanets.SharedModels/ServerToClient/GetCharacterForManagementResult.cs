using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class GetCharacterForManagementResult
    {

        public bool Success { get; set; }
        public Character Character { get; set; }
        public ErrorFromServer Error { get; set; }
    }
}
