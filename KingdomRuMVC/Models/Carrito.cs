using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomRuMVC.Models
{
    [Table("carrito")]
    public class Carrito
    {
        [Key]
        [Column("id_carrito")]
        public int Id_Carrito { get; set; }

        [Column("id_usuario")]
        public int Id_Usuario { get; set; }

        [Column("id_producto")]
        public string? Id_Producto { get; set; }

        [Column("talla")]
        public string? Talla { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [ForeignKey("Id_Producto")]
        public Producto Producto { get; set; }

    }
}
