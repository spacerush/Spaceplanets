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
        public List<SpaceObject> SpaceObjects { get; set; }
        public StarSystem()
        {
            SpaceObjects = new List<SpaceObject>();
            Version = 1;
        }

        public void CreateRandomizedSpaceObjects(Random rng)
        {
            bool foundOkPosition;
            SpaceObjects.Clear();

            SpaceObject star = new SpaceObject("Star");
            star.X = rng.Next(30, 50);
            star.Y = rng.Next(20, 30);
            star.Z = 0;

            int numberPlanets = rng.Next(1, 20);
            for (int i = 0; i <= numberPlanets; i++)
            {
                foundOkPosition = false;
                SpaceObject planet = new SpaceObject("Planet", "Planet " + Helpers.GenerationHelper.CreateRandomString(true, true, false, 5));
                while (foundOkPosition == false)
                {
                    int potentialX = rng.Next(1, 80);
                    int potentialY = rng.Next(1, 40);
                    int potentialZ = rng.Next(1, 10);
                    if (potentialX != star.X && potentialY != star.Y && potentialZ != star.Z && SpaceObjects.Where(w => w.X == potentialX && w.Y == potentialY && w.Z == potentialZ).FirstOrDefault() == null)
                    {
                        foundOkPosition = true;
                        planet.X = potentialX;
                        planet.Y = potentialY;
                        planet.Z = potentialZ;
                    }
                }
                SpaceObjects.Add(planet);

                int numberMoons = rng.Next(0, 3);
                for (int j = 0; j <= numberMoons; j++)
                {
                    SpaceObject moon = new SpaceObject("Moon " + Helpers.GenerationHelper.CreateRandomString(true, true, false, 5) + " belonging to " + planet.Name);
                    if (j == 0)
                    {
                        moon.X = planet.X + 1;
                    }
                    if (j == 1)
                    {
                        moon.X = planet.X - 1;
                    }
                    if (j == 2)
                    {
                        moon.Y = planet.Y + 1;
                    }
                    if (j == 3)
                    {
                        moon.Y = planet.Y - 1;
                    }
                    SpaceObjects.Add(moon);
                }
            }
        }

    }
}
