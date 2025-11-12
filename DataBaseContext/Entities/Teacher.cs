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
        [Key]
        [Column("employeenumber")]
        [StringLength(10)]
        public string Employeenumber { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("firstlastname")]
        [StringLength(100)]
        public string Firstlastname { get; set; }
        [Column("secondlastname")]
        [StringLength(100)]
        public string Secondlastname { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("gender")]
        [StringLength(10)]
        public string Gender { get; set; }
    }
}
