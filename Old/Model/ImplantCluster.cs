using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ImplantCluster
    {
        public int ImplantClusterId { get; set; }
        public int ImplantId { get; set; }
        public int ClusterId { get; set; }

        [ForeignKey("ClusterId")]
        [InverseProperty("ImplantClusters")]
        public virtual Cluster Cluster { get; set; }
        [ForeignKey("ImplantId")]
        [InverseProperty("ImplantClusters")]
        public virtual Implant Implant { get; set; }
    }
}