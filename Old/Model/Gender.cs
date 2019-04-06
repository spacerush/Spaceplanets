using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Gender
    {
        public Gender()
        {
            Characters = new HashSet<Character>();
        }

        public int GenderId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Gender")]
        public virtual ICollection<Character> Characters { get; set; }
    }
}