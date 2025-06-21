using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBancosRepository
    {
        /// <summary>
        /// Obtiene una lista de bancos.
        /// </summary>
        /// <returns>Lista de bancos.</returns>
        List<Banco> ObtenerBancos();
    }
}
