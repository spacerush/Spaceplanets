using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Skill
    {
        public Skill()
        {
            Clusters = new HashSet<Cluster>();
        }

        public int SkillId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Skill")]
        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}