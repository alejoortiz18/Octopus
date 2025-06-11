using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Auth
{
    public class SignUpDto : IValidatableObject
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat Password is required")]
        public string RepeatPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.Equals(Password, RepeatPassword))
            {
                yield return new ValidationResult(
                    "Las contraseñas no coinciden",
                    new[] { nameof(Password), nameof(RepeatPassword) }
                );
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                yield return new ValidationResult(
                    "La contraseña debe tener al menos 6 caracteres.",
                    new[] { nameof(Password) }
                );
            }
            if (!Password.Any(char.IsUpper))
            {
                yield return new ValidationResult(
                    "La contraseña debe contener al menos una letra mayúscula.",
                    new[] { nameof(Password) }
                );
            }
            if (!Password.Any(char.IsLower))
            {
                yield return new ValidationResult(
                    "La contraseña debe contener al menos una letra minúscula.",
                    new[] { nameof(Password) }
                );
            }
            if (!Password.Any(char.IsDigit))
            {
                yield return new ValidationResult(
                    "La contraseña debe contener al menos un número.",
                    new[] { nameof(Password) }
                );
            }
        }
    }
}
