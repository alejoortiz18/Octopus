using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
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
