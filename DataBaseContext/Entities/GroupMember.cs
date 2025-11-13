using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Index(nameof(GroupId), Name = "FK_GroupMembers_groupId")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class GroupMember
    {
        [Key]
        [Column("nua")]
        public string Nua { get; set; }
        [Key]
        [Column("groupId")]
        public int GroupId { get; set; }
        [Column("visible", TypeName = "bit(2)")]
        public ulong? Visible { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(Nua))]
        [InverseProperty(nameof(Student.GroupMembers))]
        public virtual Student NuaNavigation { get; set; }
    }
}
