using SpacePlanets.SharedModels.GameObjects;
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

        public GetCharactersForMenuResult()
        {
            this.Characters = new List<GenericItemForPicklist>();
        }

        public GetCharactersForMenuResult(List<Character> characters)
        {
            this.Success = true;
            this.Error = null;
            this.Characters = new List<GenericItemForPicklist>();
            foreach (var item in characters)
            {
                this.Characters.Add(new GenericItemForPicklist() { Id = item.Id, Name = item.Name + " the level " + item.Level.ToString() + " " + item.Profession });
            }
        }

    }
}
