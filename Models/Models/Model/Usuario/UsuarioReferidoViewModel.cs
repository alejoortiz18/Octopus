using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model.Usuario
{
    public class UsuarioReferidoViewModel
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public string CodigoReferencia { get; set; }
        public List<UsuarioReferidoViewModel> Referidos { get; set; } = new();
    }
}
