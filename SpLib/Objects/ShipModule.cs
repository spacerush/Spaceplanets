using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class ShipModule : Document
    {
        public string Name { get; set; }
        public string SlotType { get; set; }
        public int Level { get; set; }
        public List<ShipStatAlteration> ShipStatAlterations { get; set; }

        public ShipModule()
        {

        }

        public ShipModule(int level, string slotType, string name)
        {
            Level = level;
            SlotType = slotType;
            Name = name;
        }
    }
}
