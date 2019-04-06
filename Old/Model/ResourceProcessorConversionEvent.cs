using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceProcessorConversionEvent
    {
        public int ResourceProcessorConversionEventId { get; set; }
        public int ResourceProcessorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UtcDate { get; set; }
        public int InputResourceId { get; set; }
        public int OutputResourceId { get; set; }
        public int InputQuantity { get; set; }
        public int OutputQuantity { get; set; }

        [ForeignKey("InputResourceId")]
        [InverseProperty("ResourceProcessorConversionEventInputResources")]
        public virtual Resource InputResource { get; set; }
        [ForeignKey("OutputResourceId")]
        [InverseProperty("ResourceProcessorConversionEventOutputResources")]
        public virtual Resource OutputResource { get; set; }
        [ForeignKey("ResourceProcessorId")]
        [InverseProperty("ResourceProcessorConversionEvents")]
        public virtual ResourceProcessor ResourceProcessor { get; set; }
    }
}