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
        public string Name { get; set; }

        /// <summary>
        /// Age of the star in years.
        /// </summary>
        public double AgeYears { get; set; }

        /// <summary>
        /// The maximum lifetime of the star in years.
        /// </summary>
        public double Life { get; set; }

        /// <summary>
        /// The distance that the star's "ecosphere" (as far as I can tell,
        /// ye olden science speak for circumstellar habitable zone) is
        /// centered on. Given in AU. 
        /// </summary>
        public double EcosphereRadiusAU { get; set; }

        /// <summary>
        /// Luminosity of the star in solar luminosity units (L<sub>☉</sub>).
        /// The luminosity of the sun is 1.0.
        /// </summary>
        public double Luminosity { get; set; }

        /// <summary>
        /// Mass of the star in solar mass units (M<sub>☉</sub>). The mass of
        /// the sun is 1.0.
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// The mass of this star's companion star (if any) in solar mass
        /// units (M<sub>☉</sub>). 
        /// </summary>
        public double BinaryMass { get; set; }

        /// <summary>
        /// The semi-major axis of the companion star in au.
        /// </summary>
        public double SemiMajorAxisAU { get; set; }

        /// <summary>
        /// The eccentricity of the companion star's orbit.
        /// </summary>
        public double Eccentricity { get; set; }

        public Guid Id { get; set; }

        public Star()
        {

        }
    }
}
