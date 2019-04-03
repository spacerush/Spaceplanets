using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceRushEntities.Model
{
    public partial class GroupMembershipRecord
    {
        public int GroupMembershipRecordId { get; set; }
        public int PlayerId { get; set; }
        public int GroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidEnd { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("GroupMembershipRecords")]
        public virtual Group Group { get; set; }
        [ForeignKey("PlayerId")]
        [InverseProperty("GroupMembershipRecords")]
        public virtual Player Player { get; set; }
    }
}