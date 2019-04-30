using CasualGodComplex;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanets.SharedModels.GameObjects
{
    public class GalaxyContainer : Document
    {
        public SpacePlanets.SharedModels.GameObjects.Galaxy Galaxy { get; set; }

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
            SpacePlanets.SharedModels.GameObjects.Galaxy spGalaxy = new SpacePlanets.SharedModels.GameObjects.Galaxy();
            spGalaxy.Stars = new List<SpacePlanets.SharedModels.GameObjects.Star>();
            foreach (var star in galaxy.Stars)
            {
                SpacePlanets.SharedModels.GameObjects.Star newStar = new SpacePlanets.SharedModels.GameObjects.Star();
                newStar.Id = star.Id;
                newStar.Name = star.Name;
                newStar.Size = star.Size;
                newStar.Temperature = star.Temperature;
                newStar.X = (int)Math.Round(star.Position.X*1000);
                newStar.Y = (int)Math.Round(star.Position.Y*1000);
                newStar.Z = (int)Math.Round(star.Position.Z*1000);
                spGalaxy.Stars.Add(newStar);
            }
            Galaxy = spGalaxy;
        }
    }
}
