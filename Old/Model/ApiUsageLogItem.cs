using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class ApiUsageLogItem
    {
        public int ApiUsageLogItemId { get; set; }
        public int PlayerId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UsageTimeUtc { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("ApiUsageLogItems")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("PlayerId")]
        [InverseProperty("ApiUsageLogItems")]
        public virtual Player Player { get; set; }
    }
}