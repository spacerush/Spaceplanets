using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Permission
    {
        public Permission()
        {
            ApiRateLimits = new HashSet<ApiRateLimit>();
            ApiUsageLogItems = new HashSet<ApiUsageLogItem>();
            GroupPermissions = new HashSet<GroupPermission>();
        }

        public int PermissionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        [InverseProperty("Permission")]
        public virtual ICollection<ApiRateLimit> ApiRateLimits { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<ApiUsageLogItem> ApiUsageLogItems { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}