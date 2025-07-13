using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IConfiguracionSistemaRepositorio
    {
        ConfiguracionSistema ObtenerConfiguracionSistema();
    }
}
