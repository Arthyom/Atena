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
        [Key]
        [Column("nua")]
        [StringLength(10)]
        public string Nua { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("firstlastname")]
        [StringLength(100)]
        public string Firstlastname { get; set; }
        [Column("secondlastname")]
        [StringLength(100)]
        public string Secondlastname { get; set; }
        [Column("program")]
        [StringLength(300)]
        public string Program { get; set; }
        [Column("gender")]
        [StringLength(10)]
        public string Gender { get; set; }
        [Column("birthday", TypeName = "datetime")]
        public DateTime? Birthday { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
