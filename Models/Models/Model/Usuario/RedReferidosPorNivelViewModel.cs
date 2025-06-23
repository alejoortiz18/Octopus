using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model.Usuario
{
    public class UsuarioPrincipalRedReferidosViewModel
    {
        public int CantidadUsuario{ get; set; }
        public int Nivel { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<UsuarioReferidoViewModel> Usuarios { get; set; } = new();
    }
}
