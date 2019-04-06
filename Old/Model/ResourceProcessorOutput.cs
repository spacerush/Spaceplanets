using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ResourceProcessorOutput
    {
        public int ResourceProcessorOutputId { get; set; }
        public int ResourceProcessorId { get; set; }
        public int ResourceId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ResourceProcessorOutputs")]
        public virtual Resource Resource { get; set; }
        [ForeignKey("ResourceProcessorId")]
        [InverseProperty("ResourceProcessorOutputs")]
        public virtual ResourceProcessor ResourceProcessor { get; set; }
    }
}