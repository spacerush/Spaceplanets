using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ApiRateLimit
    {
        public int ApiRateLimitId { get; set; }
        public int PlayerId { get; set; }
        public int PermissionId { get; set; }
        public int MinutelyLimit { get; set; }
        public int HourlyLimit { get; set; }
        public int DailyLimit { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("ApiRateLimits")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("PlayerId")]
        [InverseProperty("ApiRateLimits")]
        public virtual Player Player { get; set; }
    }
}