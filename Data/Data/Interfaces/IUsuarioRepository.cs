using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUsuarioRepository
    {
        bool Save(Usuario user);

        Usuario ObtenerPorEmail(string email);
        Usuario ObtenerPorCodigoReferencia(string codigoReferencia);
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        Task<bool> ActualizarUsuarioAsync(DatosPersonalesUsuarioDto usuario);


        Usuario ObtenerPorId(Guid Id);

        (bool,Usuario) ReenviarCodigo(EnabledUserDto modelUser);

        
    }
}
