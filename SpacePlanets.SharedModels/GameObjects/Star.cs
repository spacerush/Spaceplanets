using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class Star
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public float Size { get; set; }
        public string Name { get; set; }
        public float Temperature { get; set; }
        public Guid Id { get; set; }

        public Star()
        {

        }
    }
}
