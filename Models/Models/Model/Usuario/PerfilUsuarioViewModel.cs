using Models.Dto.Banco;
using Models.Dto.Usuario;

namespace Models.Model.Usuario
{
    public class PerfilUsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public bool ActualizarDatosPersonales { get; set; }
        public bool ActualizarDatosBancarios { get; set; }

        public DatosPersonalesUsuarioDto DatosPersonales { get; set; } = new DatosPersonalesUsuarioDto();
        public DatosBancariosDto DatosBancarios { get; set; } = new DatosBancariosDto();
        public TipoCuentaDto TipoBancarios { get; set; } = new TipoCuentaDto();
    }

}
