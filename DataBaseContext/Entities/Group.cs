using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Index(nameof(EmployeeNumber), Name = "FK_Groups_employeeNumber")]
    [Index(nameof(PeriodId), Name = "FK_Groups_periodId")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Group
    {
        public Group()
        {
            GroupMembers = new HashSet<GroupMember>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("identifier")]
        [StringLength(50)]
        public string Identifier { get; set; }
        [Column("learningUnit")]
        [StringLength(255)]
        public string LearningUnit { get; set; }
        [Column("level")]
        [StringLength(255)]
        public string Level { get; set; }
        [Column("employeeNumber")]
        public string EmployeeNumber { get; set; }
        [Column("periodId")]
        public int? PeriodId { get; set; }
        [Column("visible", TypeName = "bit(1)")]
        public ulong? Visible { get; set; }
        [Column("idAlterno")]
        [StringLength(45)]
        public string IdAlterno { get; set; }

        [ForeignKey(nameof(EmployeeNumber))]
        [InverseProperty(nameof(Teacher.Groups))]
        public virtual Teacher EmployeeNumberNavigation { get; set; }
        [ForeignKey(nameof(PeriodId))]
        [InverseProperty("Groups")]
        public virtual Period Period { get; set; }
        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }
}
