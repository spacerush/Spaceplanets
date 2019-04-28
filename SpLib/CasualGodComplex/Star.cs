
using StarformCore.Data;
using System;
using System.Numerics;

namespace CasualGodComplex
{
    public class Star
    {
        public Vector3 Position { get; internal set; }

        public float Size { get; private set; }
        public string Name { get; private set; }

        public float Temperature { get; internal set; }

        public Star(Vector3 position, string name, float temp = 0)
        {
            Name = name;
            Position = position;
            Temperature = temp;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
    }
}
