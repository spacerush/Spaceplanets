using CasualGodComplex;
using MongoDbGenericRepository.Models;
using StarformCore.Data;
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
                StellarSystem system = StarformCore.Generator.GenerateStellarSystem(star.Name);
                newStar.Id = star.Id;
                newStar.Name = star.Name;
                newStar.AgeYears = system.Star.AgeYears;
                newStar.BinaryMass = system.Star.BinaryMass;
                newStar.Eccentricity = system.Star.Eccentricity;
                newStar.EcosphereRadiusAU = system.Star.EcosphereRadiusAU;
                newStar.Luminosity = system.Star.Luminosity;
                /* According to https://earthsky.org/astronomy-essentials/stellar-luminosity-the-true-brightness-of-stars
                 * 
                 * The luminosity of any star is the product of the radius squared times the surface temperature raised to the fourth power.
                 * Given a star whose radius is 3 solar and a surface temperature that’s 2 solar, we can figure that star’s luminosity with the equation below:
                 * whereby L = luminosity, R = radius and T = surface temperature:
                 *  L = R2 x T4
                 */
                newStar.Mass = system.Star.Mass;
                newStar.SemiMajorAxisAU = system.Star.SemiMajorAxisAU;
                newStar.X = (int)Math.Round(star.Position.X*1000);
                newStar.Y = (int)Math.Round(star.Position.Y*1000);
                newStar.Z = (int)Math.Round(star.Position.Z*1000);
                spGalaxy.Stars.Add(newStar);
            }
            Galaxy = spGalaxy;
        }
    }
}
