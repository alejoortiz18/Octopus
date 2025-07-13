using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUsuarioBusiness
    {
       // bool SaveProfile(CreateProfileUserDto user);
        Usuario ObtenerPorEmail(string email);
        Usuario ObtenerPorCodigoReferencia(string codigoReferencia);
        Usuario ObtenerPorId(int Id);
        Usuario ObtenerCuentaApoyo();
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        public Task<bool> ActualizarUsuarioAsync(DatosPersonalesUsuarioDto usuario, int? usuairoID = 0);
        bool ReenviarCodigo(EnabledUserDto modelUser);
        bool Save(Usuario usuario);
    }
}
