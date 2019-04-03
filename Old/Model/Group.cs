using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class Group
    {
        public Group()
        {
            GroupMembershipRecords = new HashSet<GroupMembershipRecord>();
            GroupPermissions = new HashSet<GroupPermission>();
        }

        public int GroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<GroupMembershipRecord> GroupMembershipRecords { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}