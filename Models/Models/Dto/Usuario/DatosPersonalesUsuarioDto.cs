using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Models.Dto.Usuario
{
    public class DatosPersonalesUsuarioDto
    {
        public string CodigoReferencia { get; set; }
        public int EstadoUsuario { get; set; }        
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaHabilitacion { get; set; }
    }
}
