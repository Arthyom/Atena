using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Index(nameof(Periodid), Name = "Groups_Periods_FK")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Group
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }
        [Column("periodid")]
        public int? Periodid { get; set; }
        [Column("employeenumber")]
        [StringLength(10)]
        public string Employeenumber { get; set; }
        [Column("learningunit")]
        [StringLength(100)]
        public string Learningunit { get; set; }
        [Column("level")]
        [StringLength(10)]
        public string Level { get; set; }
        [Column("identifier")]
        [StringLength(5)]
        public string Identifier { get; set; }

        [ForeignKey(nameof(Periodid))]
        [InverseProperty("Groups")]
        public virtual Period Period { get; set; }
    }
}
