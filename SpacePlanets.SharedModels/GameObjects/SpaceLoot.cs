using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class SpaceLoot : Document
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<ShipModule> ShipModules { get; set; }

        public SpaceLoot()
        {
            ShipModules = new List<ShipModule>();
        }
    }
}
