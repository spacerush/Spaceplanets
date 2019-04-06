using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ClusterSlot
    {
        public ClusterSlot()
        {
            Clusters = new HashSet<Cluster>();
        }

        public int ClusterSlotId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ClusterSlot")]
        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}