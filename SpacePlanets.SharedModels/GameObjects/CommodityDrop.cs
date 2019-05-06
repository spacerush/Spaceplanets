using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class CommodityDrop : Document
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string CommodityName { get; set; }

        public CommodityDrop()
        {

        }
    }
}
