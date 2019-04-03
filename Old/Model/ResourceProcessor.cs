using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceProcessor
    {
        public ResourceProcessor()
        {
            ResourceProcessorConversionEvents = new HashSet<ResourceProcessorConversionEvent>();
            ResourceProcessorInputs = new HashSet<ResourceProcessorInput>();
            ResourceProcessorOutputs = new HashSet<ResourceProcessorOutput>();
            ResourceStorages = new HashSet<ResourceStorage>();
        }

        public int ResourceProcessorId { get; set; }
        public int SpaceObjectId { get; set; }
        public int ResourceProcessorTypeId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("ResourceProcessorTypeId")]
        [InverseProperty("ResourceProcessors")]
        public virtual ResourceProcessorType ResourceProcessorType { get; set; }
        [ForeignKey("SpaceObjectId")]
        [InverseProperty("ResourceProcessors")]
        public virtual SpaceObject SpaceObject { get; set; }
        [InverseProperty("ResourceProcessor")]
        public virtual ICollection<ResourceProcessorConversionEvent> ResourceProcessorConversionEvents { get; set; }
        [InverseProperty("ResourceProcessor")]
        public virtual ICollection<ResourceProcessorInput> ResourceProcessorInputs { get; set; }
        [InverseProperty("ResourceProcessor")]
        public virtual ICollection<ResourceProcessorOutput> ResourceProcessorOutputs { get; set; }
        [InverseProperty("ResourceProcessor")]
        public virtual ICollection<ResourceStorage> ResourceStorages { get; set; }
    }
}