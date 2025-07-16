using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KingdomRuMVC.Models
{
    public class Producto
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }


        [Required]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        [Column("imagen_url")]
        [System.Text.Json.Serialization.JsonPropertyName("imagen_url")]
        public string Imagen_Url { get; set; }

        public string Categoria { get; set; }

        public bool? Usa_Talla { get; set; }

        public int Stock { get; set; }
    }
}
