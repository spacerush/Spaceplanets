using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class GroupPermission
    {
        public int GroupPermissionId { get; set; }
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidEnd { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("GroupPermissions")]
        public virtual Group Group { get; set; }
        [ForeignKey("PermissionId")]
        [InverseProperty("GroupPermissions")]
        public virtual Permission Permission { get; set; }
    }
}