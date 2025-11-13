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
            Visit1s = new HashSet<Visit1>();
            Visits = new HashSet<Visit>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("actual")]
        public bool? Actual { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }
        [Column("end", TypeName = "datetime")]
        public DateTime End { get; set; }
        [Column("start", TypeName = "datetime")]
        public DateTime Start { get; set; }
        [Column("visible", TypeName = "bit(1)")]
        public ulong? Visible { get; set; }
        [Column("idAlterno")]
        [StringLength(45)]
        public string IdAlterno { get; set; }

        [InverseProperty(nameof(Group.Period))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(Visit1.Period))]
        public virtual ICollection<Visit1> Visit1s { get; set; }
        [InverseProperty(nameof(Visit.Period))]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
