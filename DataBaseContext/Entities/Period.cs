using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Period
    {
        public Period()
        {
            Groups = new HashSet<Group>();
            Visits = new HashSet<Visit>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("start", TypeName = "datetime")]
        public DateTime? Start { get; set; }
        [Column("end", TypeName = "datetime")]
        public DateTime? End { get; set; }

        [InverseProperty(nameof(Group.Period))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(Visit.Period))]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
