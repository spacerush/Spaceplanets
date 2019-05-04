using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ClientToServer
{
    public class CharacterForManagementRequest
    {
        public Guid CharacterId { get; set; }

        public CharacterForManagementRequest(Guid characterId)
        {
            this.CharacterId = characterId;
        }
    }
}
