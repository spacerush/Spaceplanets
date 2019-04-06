using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class PlayerToken
    {
        public int PlayerTokenId { get; set; }
        public int PlayerId { get; set; }
        [Required]
        [StringLength(30)]
        public string Token { get; set; }
        public DateTime Expiry { get; set; }

        [ForeignKey("PlayerId")]
        [InverseProperty("PlayerTokens")]
        public virtual Player Player { get; set; }
    }
}