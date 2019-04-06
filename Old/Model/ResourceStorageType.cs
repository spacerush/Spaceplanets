using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceStorageType
    {
        public ResourceStorageType()
        {
            ResourceStorages = new HashSet<ResourceStorage>();
        }

        public int ResourceStorageTypeId { get; set; }
        public int ResourceId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int DefaultCapacity { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ResourceStorageTypes")]
        public virtual Resource Resource { get; set; }
        [InverseProperty("ResourceStorageType")]
        public virtual ICollection<ResourceStorage> ResourceStorages { get; set; }
    }
}