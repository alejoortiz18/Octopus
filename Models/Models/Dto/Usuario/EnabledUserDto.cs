using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Usuario
{
    public class EnabledUserDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; }
    }
}
