using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model.Usuario
{
    public class RedPorReferidosByIdUsuarioDto
    {
        public int Nivel { get; set; }
        public int UsuarioId { get; set; }
        public int ReferenteID { get; set; }
        public string NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? CodigoReferencia { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? NombreReferente { get; set; }

    }
}
