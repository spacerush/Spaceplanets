using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class StarSystem
    {
        public StarSystem()
        {
            Players = new HashSet<Player>();
            Ships = new HashSet<Ship>();
            SpaceObjects = new HashSet<SpaceObject>();
        }

        public int StarSystemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? SizeX { get; set; }
        public int? SizeY { get; set; }
        public int? SizeZ { get; set; }
        public int? GalaxyPositionX { get; set; }
        public int? GalaxyPositionY { get; set; }
        public int? GalaxyPositionZ { get; set; }

        [InverseProperty("StarSystem")]
        public virtual ICollection<Player> Players { get; set; }
        [InverseProperty("StarSystem")]
        public virtual ICollection<Ship> Ships { get; set; }
        [InverseProperty("StarSystem")]
        public virtual ICollection<SpaceObject> SpaceObjects { get; set; }
    }
}