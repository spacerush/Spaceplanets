using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class Galaxy : Document
    {
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int SizeZ { get; set; }
        public List<StarSystem> StarSystems { get; set; }

        public Galaxy()
        {
            Version = 1;
            StarSystems = new List<StarSystem>();
        }

    }
}
