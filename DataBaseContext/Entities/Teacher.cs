using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Teacher
    {
        public Teacher()
        {
            Groups = new HashSet<Group>();
        }

        [Key]
        [Column("employeeNumber")]
        public string EmployeeNumber { get; set; }
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
        [Required]
        [Column("secondLastName")]
        [StringLength(255)]
        public string SecondLastName { get; set; }
        [Column("visible", TypeName = "bit(1)")]
        public ulong? Visible { get; set; }

        [InverseProperty(nameof(Group.EmployeeNumberNavigation))]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
