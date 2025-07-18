using System.ComponentModel.DataAnnotations.Schema;

namespace KingdomRuMVC.Models
{
    [Table("detalle_factura")]
    public class DetalleFactura
    {
        public string Id_Factura { get; set; }

        public string Id_Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio_Unitario { get; set; }

        public string Estado_Detalle { get; set; }

        [ForeignKey("Id_Factura")]
        public virtual Factura Factura { get; set; }

        [ForeignKey("Id_Producto")]
        public virtual Producto Producto { get; set; }
    }
}
