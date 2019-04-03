using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceProcessorType
    {
        public ResourceProcessorType()
        {
            ResourceProcessors = new HashSet<ResourceProcessor>();
        }

        public int ResourceProcessorTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ResourceProcessorType")]
        public virtual ICollection<ResourceProcessor> ResourceProcessors { get; set; }
    }
}