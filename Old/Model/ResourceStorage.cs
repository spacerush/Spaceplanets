using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceStorage
    {
        public ResourceStorage()
        {
            ResourceStorageEvents = new HashSet<ResourceStorageEvent>();
        }

        public int ResourceStorageId { get; set; }
        public int ResourceStorageTypeId { get; set; }
        public int ResourceProcessorId { get; set; }
        public int? Capacity { get; set; }

        [ForeignKey("ResourceProcessorId")]
        [InverseProperty("ResourceStorages")]
        public virtual ResourceProcessor ResourceProcessor { get; set; }
        [ForeignKey("ResourceStorageTypeId")]
        [InverseProperty("ResourceStorages")]
        public virtual ResourceStorageType ResourceStorageType { get; set; }
        [InverseProperty("ResourceStorage")]
        public virtual ICollection<ResourceStorageEvent> ResourceStorageEvents { get; set; }
    }
}