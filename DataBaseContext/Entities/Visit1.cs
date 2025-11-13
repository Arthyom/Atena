using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Table("Visits")]
    [Index(nameof(Periodid), Name = "FK_Visits_periodid")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Visit1
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("end", TypeName = "datetime")]
        public DateTime? End { get; set; }
        [Column("nua")]
        [StringLength(255)]
        public string Nua { get; set; }
        [Column("skill")]
        [StringLength(255)]
        public string Skill { get; set; }
        [Column("start", TypeName = "datetime")]
        public DateTime? Start { get; set; }
        [Column("periodid")]
        public int? Periodid { get; set; }
        [Column("visible", TypeName = "bit(2)")]
        public ulong? Visible { get; set; }

        [ForeignKey(nameof(Periodid))]
        [InverseProperty("Visit1s")]
        public virtual Period Period { get; set; }
    }
}
