using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class Character : Document
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Profession { get; set; }
        public List<string> Perks { get; set; }
        public List<ImprovementExpenditure> ImprovementExpenditures { get; set; }
        public Guid ShipId { get; set; }
        public Guid SpaceObjectId { get; set; }
        public Guid PlayerId { get; set; }
        public Character(string characterName, int level, string profession)
        {
            Name = characterName;
            Level = level;
            Profession = profession;
            Version = 1;
        }

        public Character()
        {

        }

    }
}
