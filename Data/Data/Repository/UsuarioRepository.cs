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
                usuario.FechaExpiracionToken = DateTime.Now.AddHours(2);

                // Guardar los cambios en la base de datos
                var saveresult = _context.SaveChanges() > 0;
                return (saveresult,usuario);
            }
            catch (Exception ex)
            {
                return (false,null);
            }
        }


        public async Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioId == usuario.UsuarioId);

                if (usuarioExistente == null)
                {
                    return false;
                }
                usuarioExistente.EstadoUsuarioId = usuario.EstadoUsuarioId;
                usuarioExistente.TokenVerificacion = usuario.TokenVerificacion;
                usuarioExistente.FechaExpiracionToken = usuario.FechaExpiracionToken;
                usuarioExistente.FechaHabilitacion = usuario.FechaHabilitacion;
                _context.Usuarios.Update(usuarioExistente);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            } 
         
        }

        public async Task<bool> ActualizarUsuarioAsync(DatosPersonalesUsuarioDto usuario)
        {
            try
            {
                var usuarioExistente = _context.Usuarios
                    .FirstOrDefault(u => u.Email == usuario.Email);

                if (usuarioExistente == null)
                {
                    return false;
                }
                usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;
                usuarioExistente.NombreCompleto = usuario.NombreCompleto;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.NumeroCelular = usuario.NumeroCelular;
                usuarioExistente.CodigoReferencia = usuario.CodigoReferencia;
                usuarioExistente.ReferenteId= usuario.ReferenteId;
                _context.Usuarios.Update(usuarioExistente);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }


        public Usuario ObtenerPorId(int Id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.UsuarioId == Id);
        }

        public Usuario ObtenerPorCodigoReferencia(string codigoReferencia)
        {
            return _context.Usuarios.FirstOrDefault(u => u.CodigoReferencia == codigoReferencia);
        }

        public Usuario ObtenerCuentaApoyo()
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == "apoyo@yopmail.com");
        }
    }
}
