using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Profession
    {
        public Profession()
        {
            Characters = new HashSet<Character>();
        }

        public int ProfessionId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Profession")]
        public virtual ICollection<Character> Characters { get; set; }
    }
}