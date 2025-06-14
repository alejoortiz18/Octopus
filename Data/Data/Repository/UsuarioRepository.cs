using Data.Interfaces;
using Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly ICodigoHelper _codigoHelper;
        public UsuarioRepository(AppDbContext context, ICodigoHelper codigoHelper)
        {
            _context = context;
            _codigoHelper = codigoHelper;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return  _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }
                

        public bool Save(Usuario user)
        {
            try
            {

                var result = _context.Usuarios.Add(user);
                var saveresult = _context.SaveChanges() > 0;
                return saveresult;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public (bool,Usuario) ReenviarCodigo(EnabledUserDto modelUser)
        {
            try
            {
                // Buscar el usuario por email
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == modelUser.Email);
                if (usuario == null)
                { return (false,null); }

                
                usuario.TokenVerificacion = _codigoHelper.GenerarCodigoUnico(); 

                // Guardar los cambios en la base de datos
                var saveresult = _context.SaveChanges() > 0;
                return (saveresult,usuario);
            }
            catch (Exception ex)
            {
                return (false,null);
            }
        }


        public Task ActualizarUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }


        public Usuario ObtenerPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

    }
}
