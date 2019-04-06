using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Player
    {
        public Player()
        {
            ApiRateLimits = new HashSet<ApiRateLimit>();
            ApiUsageLogItems = new HashSet<ApiUsageLogItem>();
            Characters = new HashSet<Character>();
            GroupMembershipRecords = new HashSet<GroupMembershipRecord>();
            PlayerTokens = new HashSet<PlayerToken>();
            RefreshTokens = new HashSet<RefreshToken>();
            Ships = new HashSet<Ship>();
        }

        public int PlayerId { get; set; }
        [Required]
        [StringLength(13)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        public int StarSystemId { get; set; }
        public bool IsGameAdmin { get; set; }

        [ForeignKey("StarSystemId")]
        [InverseProperty("Players")]
        public virtual StarSystem StarSystem { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<ApiRateLimit> ApiRateLimits { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<ApiUsageLogItem> ApiUsageLogItems { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<Character> Characters { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<GroupMembershipRecord> GroupMembershipRecords { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<PlayerToken> PlayerTokens { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<Ship> Ships { get; set; }
    }
}