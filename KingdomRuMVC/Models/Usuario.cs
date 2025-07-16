using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomRuMVC.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")] // <-- asegúrate que este nombre sea igual al de la BD
        public string IdUsuario { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("edad")]
        public int? Edad { get; set; }

        [Required]
        [Column("correo")]
        public string Correo { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Required]
        [Column("clave")]
        public string Clave { get; set; }
    }
}
