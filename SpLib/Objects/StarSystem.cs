using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpLib.Objects
{
    public class StarSystem : Document
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Guid GalaxyId { get; set; }
        public StarSystem(Guid galaxyId)
        {
            this.GalaxyId = galaxyId;
            Version = 1;
        }

    }
}
