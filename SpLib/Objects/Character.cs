using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Character : Document
    {
        public string Name { get; set; }
        public Character(string characterName)
        {
            Name = characterName;
            Version = 1;
        }

        public Character()
        {
            Version = 1;
        }
    }
}
