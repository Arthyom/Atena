using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Keyless]
    [Index(nameof(Nua), Name = "GroupMember_Stuents_FK")]
    [Index(nameof(Groupid), Name = "GroupMembers_Groups_FK")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class GroupMember
    {
        [Column("groupid")]
        public uint? Groupid { get; set; }
        [Column("nua")]
        [StringLength(10)]
        public string Nua { get; set; }

        [ForeignKey(nameof(Groupid))]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(Nua))]
        public virtual Student NuaNavigation { get; set; }
    }
}
