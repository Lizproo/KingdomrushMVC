using System;
using System.ComponentModel.DataAnnotations;

namespace KingdomRuMVC.Models
{
    public class Factura
    {
        [Key]
        public string Id_Factura { get; set; }

        public string Id_Usuario { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? Subtotal { get; set; }

        public int? Iva { get; set; }

        public decimal? Total { get; set; }

        public string Estado { get; set; }
    }
}
