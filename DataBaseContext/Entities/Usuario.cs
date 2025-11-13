using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    [Table("usuarios")]
    [Index(nameof(Nombre), Name = "nombre_UNIQUE", IsUnique = true)]
    [Index(nameof(Pass), Name = "pass_UNIQUE", IsUnique = true)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public partial class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }
        [Required]
        [Column("pass")]
        [StringLength(40)]
        public string Pass { get; set; }
        [Column("visible", TypeName = "bit(1)")]
        public ulong? Visible { get; set; }
    }
}
