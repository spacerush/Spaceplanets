using CasualGodComplex;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Objects
{
    public class GalaxyContainer : Document
    {
        public SpLib.Objects.Galaxy Galaxy { get; set; }

        public string Name { get; set; }

        public GalaxyContainer()
        {

        }

        public GalaxyContainer(string name)
        {
            Name = name;
        }

        public void SetGalaxy(CasualGodComplex.Galaxy galaxy)
        {
            SpLib.Objects.Galaxy spGalaxy = new SpLib.Objects.Galaxy();
            spGalaxy.Stars = new List<SpLib.Objects.Star>();
            foreach (var star in galaxy.Stars)
            {
                SpLib.Objects.Star newStar = new SpLib.Objects.Star();
                newStar.Id = star.Id;
                newStar.Name = star.Name;
                newStar.Size = star.Size;
                newStar.Temperature = star.Temperature;
                newStar.X = (int)Math.Round(star.Position.X);
                newStar.Y = (int)Math.Round(star.Position.Y);
                newStar.Z = (int)Math.Round(star.Position.Z);
                spGalaxy.Stars.Add(newStar);
            }
            Galaxy = spGalaxy;
        }
    }
}
