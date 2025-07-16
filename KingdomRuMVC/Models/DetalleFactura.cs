using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomRuMVC.Models
{
    [Table("detalle_factura")]
    public class DetalleFactura
    {
        [Key]
        public string Id_Factura { get; set; }

        [Required]
        public string Id_Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Precio_Unitario { get; set; }

        public string Estado_Detalle { get; set; }
    }
}
