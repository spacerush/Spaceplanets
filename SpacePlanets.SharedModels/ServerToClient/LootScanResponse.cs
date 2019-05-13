using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class LootScanResponse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<SpaceLoot> SpaceLoots { get; set; }
    }
}
