using Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.RedReferidos
{
    public class RedRepository : IRedRepository
    {
        private readonly AppDbContext _context;
        public RedRepository(AppDbContext context)
        {
            _context = context;
        }
              

        public List<RedPorReferidosByIdUsuarioDto> GetTodaLaRedPorUsuarioIdAsync(int usuarioId)
        {
            var usuarioIdParam = new SqlParameter("@UsuarioID", SqlDbType.Int) { Value = usuarioId };

            var result = _context.Set<RedPorReferidosByIdUsuarioDto>()
           .FromSqlRaw("EXEC SP_GetRedPorReferidosByIdUsuario @UsuarioID", usuarioIdParam)
           .ToList();

            return result;
        }

        public RedReferido Insert(RedReferido red) 
        {            
           
            _context.RedReferidos.Add(red);
            _context.SaveChanges();
            return red;
        }

    }
}
