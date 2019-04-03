using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class MotdItem
    {
        public int MotdItemId { get; set; }
        [Required]
        [StringLength(500)]
        public string MotdText { get; set; }
        public int SortOrder { get; set; }
        [Required]
        public bool? IsActive { get; set; }
    }
}