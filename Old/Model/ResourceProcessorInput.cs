using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceProcessorInput
    {
        public int ResourceProcessorInputId { get; set; }
        public int ResourceProcessorId { get; set; }
        public int ResourceId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ResourceProcessorInputs")]
        public virtual Resource Resource { get; set; }
        [ForeignKey("ResourceProcessorId")]
        [InverseProperty("ResourceProcessorInputs")]
        public virtual ResourceProcessor ResourceProcessor { get; set; }
    }
}