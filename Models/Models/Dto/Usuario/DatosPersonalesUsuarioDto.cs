using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Usuario
{
    public class DatosPersonalesUsuarioDto
    {
        public int Rol { get; set; }
        public string CodigoReferencia { get; set; } = "";
        public string CodigoReferente { get; set; } = "";
        public int ReferenteId { get; set; }

        public int EstadoUsuario { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } = "";
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null;

        [Display(Name = "Número Celular")]
        public string NumeroCelular { get; set; } = null;
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaHabilitacion { get; set; }
    }
}
