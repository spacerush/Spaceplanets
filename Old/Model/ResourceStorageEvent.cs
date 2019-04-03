using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceStorageEvent
    {
        public int ResourceStorageEventId { get; set; }
        public int ResourceStorageId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UtcDate { get; set; }
        public int QuantityStored { get; set; }
        public int? ResourceProcessorConversionEventId { get; set; }

        [ForeignKey("ResourceStorageId")]
        [InverseProperty("ResourceStorageEvents")]
        public virtual ResourceStorage ResourceStorage { get; set; }
    }
}