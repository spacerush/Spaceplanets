using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Resource
    {
        public Resource()
        {
            ResourceHubs = new HashSet<ResourceHub>();
            ResourceProcessorConversionEventInputResources = new HashSet<ResourceProcessorConversionEvent>();
            ResourceProcessorConversionEventOutputResources = new HashSet<ResourceProcessorConversionEvent>();
            ResourceProcessorInputs = new HashSet<ResourceProcessorInput>();
            ResourceProcessorOutputs = new HashSet<ResourceProcessorOutput>();
            ResourceStorageTypes = new HashSet<ResourceStorageType>();
            ShipTypesResources = new HashSet<ShipTypesResource>();
        }

        public int ResourceId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Resource")]
        public virtual ICollection<ResourceHub> ResourceHubs { get; set; }
        [InverseProperty("InputResource")]
        public virtual ICollection<ResourceProcessorConversionEvent> ResourceProcessorConversionEventInputResources { get; set; }
        [InverseProperty("OutputResource")]
        public virtual ICollection<ResourceProcessorConversionEvent> ResourceProcessorConversionEventOutputResources { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ResourceProcessorInput> ResourceProcessorInputs { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ResourceProcessorOutput> ResourceProcessorOutputs { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ResourceStorageType> ResourceStorageTypes { get; set; }
        [InverseProperty("Resource")]
        public virtual ICollection<ShipTypesResource> ShipTypesResources { get; set; }
    }
}