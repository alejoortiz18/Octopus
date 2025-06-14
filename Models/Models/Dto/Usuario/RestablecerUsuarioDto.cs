using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Usuario
{
    public class RestablecerUsuarioDto : IValidatableObject
    {
      

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
        public string Password2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password1 != Password2)
            {
                yield return new ValidationResult("Las contraseñas no coinciden.", new[] { nameof(Password2) });
            }
        }
    }
}
