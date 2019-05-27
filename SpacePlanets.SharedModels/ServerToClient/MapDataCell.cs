using SpacePlanets.SharedModels.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.ServerToClient
{
    public class MapDataCell
    {
        public int CellX { get; set; }
        public int CellY { get; set; }
        public int CellZ { get; set; }
        public List<Ship> Ships { get; set; }
        public List<SpaceObject> SpaceObjects { get; set; }
        public List<Star> Stars { get; set; }
        public List<SpaceLoot> SpaceLoots { get; set; }
    }
}
