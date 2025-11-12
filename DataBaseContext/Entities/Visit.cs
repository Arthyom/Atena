using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Index(nameof(Periodid), Name = "Visits_Periods")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Visit
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("periodid")]
        public int? Periodid { get; set; }
        [Column("nua")]
        [StringLength(10)]
        public string Nua { get; set; }
        [Column("skill")]
        [StringLength(50)]
        public string Skill { get; set; }
        [Column("start", TypeName = "datetime")]
        public DateTime? Start { get; set; }
        [Column("end", TypeName = "datetime")]
        public DateTime? End { get; set; }

        [ForeignKey(nameof(Periodid))]
        [InverseProperty("Visits")]
        public virtual Period Period { get; set; }
    }
}
