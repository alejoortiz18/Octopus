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

        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
                
        Usuario ObtenerPorId(Guid Id);

        (bool,Usuario) ReenviarCodigo(EnabledUserDto modelUser);
    }
}
