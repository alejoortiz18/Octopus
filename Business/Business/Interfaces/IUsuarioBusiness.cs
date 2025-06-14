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
        Usuario ObtenerPorId(Guid Id);
        Task ActualizarUsuarioAsync(Usuario usuario);

        bool ReenviarCodigo(EnabledUserDto modelUser);
    }
}
