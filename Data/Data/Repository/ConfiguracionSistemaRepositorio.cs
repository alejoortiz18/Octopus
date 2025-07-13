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
    public class ConfiguracionSistemaRepositorio : IConfiguracionSistemaRepositorio
    {
        private readonly AppDbContext _context;
        public ConfiguracionSistemaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public ConfiguracionSistema ObtenerConfiguracionSistema()
        {
            return _context.ConfiguracionSistemas.FirstOrDefault(x=>x.ConfiguracionId == 1);
        }
    }
}
