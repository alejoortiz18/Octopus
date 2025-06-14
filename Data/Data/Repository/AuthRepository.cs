using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Auth;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario LoginAsync(string email)
        {
            try
            {
                Usuario existeUsuario = _context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
                return existeUsuario;
            }
            catch (Exception ex)
            {

                return null;
            }
           
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }


        //public bool UsuarioIsValid(UserResetDto user)
        //{
        //    var perfil = _context.Perfils.Where(p => p.Nombre == user.TypePerfil).FirstOrDefault();
        //    if (perfil == null)
        //    {
        //        return false;
        //    }
        //    Usuario? userResult = _context.Usuarios.Where(u => u.Correo == user.Correo &&
        //                                        u.Contrasena == user.Contrasena &&
        //                                        u.CodigoRestablecerPassword == user.CodigoRestablecerPassword &&
        //                                        u.CodigoActivo == true).FirstOrDefault();
        //    if (userResult == null)
        //    {
        //        return false;
        //    }

        //    // Actualizar el usuario
        //    userResult.Contrasena = user.Contrasena;
        //    userResult.CodigoActivo = false;
        //    _context.Usuarios.Update(userResult);
        //    _context.SaveChanges();

        //    return true;
        //}


        //public bool ChangePassword(UserCod user)
        //{
        //    var perfil = _context.Usuarios.Where(p => p.Correo == user.Email).FirstOrDefault();
        //    if (perfil == null)
        //    {
        //        return false;
        //    }

        //    // Actualizar el usuario
        //    perfil.Contrasena = user.Contrasena;
        //    perfil.CodigoActivo = false;
        //    _context.Usuarios.Update(perfil);
        //    _context.SaveChanges();

        //    return true;
        //}

        //public bool CodigoIsValid(UserCod cod)
        //{
        //    var user = _context.Usuarios.Where(u => u.Correo == cod.Email &&
        //                                 u.CodigoRestablecerPassword == cod.CodigoRestablecerPassword &&
        //                                 u.CodigoActivo == true).FirstOrDefault();
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
