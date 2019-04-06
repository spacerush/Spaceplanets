using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Cluster
    {
        public Cluster()
        {
            ImplantClusters = new HashSet<ImplantCluster>();
        }

        public int ClusterId { get; set; }
        public int ImplantBaseItemId { get; set; }
        public int SkillId { get; set; }
        public int LowestQualityLevel { get; set; }
        public int LowestQualityBuff { get; set; }
        public int HighestQualityLevel { get; set; }
        public int HighestQualityBuff { get; set; }
        public int ClusterSlotId { get; set; }

        [ForeignKey("ClusterSlotId")]
        [InverseProperty("Clusters")]
        public virtual ClusterSlot ClusterSlot { get; set; }
        [ForeignKey("ImplantBaseItemId")]
        [InverseProperty("Clusters")]
        public virtual ImplantBaseItem ImplantBaseItem { get; set; }
        [ForeignKey("SkillId")]
        [InverseProperty("Clusters")]
        public virtual Skill Skill { get; set; }
        [InverseProperty("Cluster")]
        public virtual ICollection<ImplantCluster> ImplantClusters { get; set; }
    }
}