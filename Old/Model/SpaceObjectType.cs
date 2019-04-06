using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class SpaceObjectType
    {
        public SpaceObjectType()
        {
            SpaceObjects = new HashSet<SpaceObject>();
        }

        public int SpaceObjectTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [InverseProperty("ObjectType")]
        public virtual ICollection<SpaceObject> SpaceObjects { get; set; }
    }
}