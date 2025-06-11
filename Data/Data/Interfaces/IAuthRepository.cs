using Models.Dto.Auth;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario?> LoginAsync(LoginDto user);

        Task<Usuario> CrearUsuarioAsync(Usuario usuario);

        //bool UsuarioIsValid(UserResetDto user);
        //bool CodigoIsValid(UserCod cod);
        //bool ChangePassword(UserCod user);
    }
}
