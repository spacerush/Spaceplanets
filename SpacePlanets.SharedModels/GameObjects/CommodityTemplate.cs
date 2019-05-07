using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class CommodityTemplate : Document
    {
        public string Name { get; set; }

        public CommodityTemplate()
        {

        }
    }
}
