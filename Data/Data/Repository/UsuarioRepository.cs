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
        private readonly OctopusDbContext _context;
        public UsuarioRepository(OctopusDbContext context)
        {
            _context = context;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return  _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }
                

        public bool Save(Usuario user)
        {
            _context.Usuarios.Add(user);
            return _context.SaveChanges() > 0;
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
