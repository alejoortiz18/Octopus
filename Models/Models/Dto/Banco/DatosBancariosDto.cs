using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Banco
{
    public class DatosBancariosDto 
    {
        public int Banco { get; set; }
        
        public List<BancoDto> Bancos { get; set; } = new List<BancoDto>();
       



    }
}
