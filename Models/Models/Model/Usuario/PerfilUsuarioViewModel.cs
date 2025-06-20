using Models.Dto.Banco;
using Models.Dto.Usuario;

namespace Models.Model.Usuario
{
    public class PerfilUsuarioViewModel
    {
        public DatosPersonalesUsuarioDto DatosPersonales { get; set; } = new DatosPersonalesUsuarioDto();
        public DatosBancariosDto DatosBancarios { get; set; } = new DatosBancariosDto();
    }

}
