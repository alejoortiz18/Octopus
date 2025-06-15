using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Usuario
{
    public class ResetPasswordResponseDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        //public string OldPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Debe repetir la nueva contraseña.")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string RepeatNewPassword { get; set; }

        [Required(ErrorMessage = "El código de verificación es obligatorio.")]
        public string Codigo { get; set; }

    }
}
