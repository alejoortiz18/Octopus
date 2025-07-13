using Models.Dto.Comision;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IComisionRepository
    {
        List<ComisionDto> BuscarNivelesSuperioesComisiones(int UsuarioID);
        bool IngresarComisiones(List<Comision> listComisiones);
    }
}
