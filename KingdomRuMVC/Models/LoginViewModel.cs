using System.ComponentModel.DataAnnotations;

namespace KingdomRuMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Contraseña es obligatoria.")]
        public string Clave { get; set; }
    }
}
