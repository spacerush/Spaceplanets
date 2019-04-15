using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class ShipTemplate : Document
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public List<ShipModuleSlot> ModuleSlots { get; set; }
        public ShipTemplate(string name, string type)
        {
            Type = type;
            Name = name;
        }
    }
}
