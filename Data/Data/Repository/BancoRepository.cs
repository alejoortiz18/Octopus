using Data.Interfaces;
using Helpers.Interfaces;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BancoRepository : IBancosRepository
    {
        private readonly AppDbContext _context;
        public BancoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Banco> ObtenerBancos()
        {
            return _context.Bancos.ToList();
        }
    }
}
