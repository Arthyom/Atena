using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Student
    {
        public Student()
        {
            GroupMembers = new HashSet<GroupMember>();
            Visits = new HashSet<Visit>();
        }

        [Key]
        [Column("nua")]
        public string Nua { get; set; }
        [Column("birthday", TypeName = "datetime")]
        public DateTime? Birthday { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("firstLastName")]
        [StringLength(255)]
        public string FirstLastName { get; set; }
        [Required]
        [Column("gender")]
        [StringLength(255)]
        public string Gender { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("program")]
        [StringLength(255)]
        public string Program { get; set; }
        [Required]
        [Column("secondLastName")]
        [StringLength(255)]
        public string SecondLastName { get; set; }
        [Column("visible", TypeName = "bit(1)")]
        public ulong? Visible { get; set; }

        [InverseProperty(nameof(GroupMember.NuaNavigation))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(Visit.NuaNavigation))]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
