using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class SpaceObject
    {
        public SpaceObject()
        {
            ResourceHubs = new HashSet<ResourceHub>();
            ResourceProcessors = new HashSet<ResourceProcessor>();
            Shipyards = new HashSet<Shipyard>();
        }

        public int SpaceObjectId { get; set; }
        [Required]
        [StringLength(50)]
        public string ObjectName { get; set; }
        public int ObjectTypeId { get; set; }
        public int StarSystemId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }
        public double? AgeYears { get; set; }
        public double? Life { get; set; }
        [Column("EcosphereRadiusAU")]
        public double? EcosphereRadiusAu { get; set; }
        public double? Luminosity { get; set; }
        public double? Mass { get; set; }
        public double? BinaryMass { get; set; }
        [Column("SemiMajorAxisAU")]
        public double? SemiMajorAxisAu { get; set; }
        public double? Eccentricity { get; set; }

        [ForeignKey("ObjectTypeId")]
        [InverseProperty("SpaceObjects")]
        public virtual SpaceObjectType ObjectType { get; set; }
        [ForeignKey("StarSystemId")]
        [InverseProperty("SpaceObjects")]
        public virtual StarSystem StarSystem { get; set; }
        [InverseProperty("SpaceObject")]
        public virtual ICollection<ResourceHub> ResourceHubs { get; set; }
        [InverseProperty("SpaceObject")]
        public virtual ICollection<ResourceProcessor> ResourceProcessors { get; set; }
        [InverseProperty("SpaceObject")]
        public virtual ICollection<Shipyard> Shipyards { get; set; }
    }
}