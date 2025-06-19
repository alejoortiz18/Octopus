using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Usuario
{
    public class DatosBancariosDto
    {
        public int Rol { get; set; }
        public int? Banco { get; set; }
        public int? TipoCuentaBancariaId { get; set; }
        public string? NumeroCuentaBancaria { get; set; }
        public  Banco Bancos { get; set; }
        public virtual ICollection<BackupRegistro> BackupRegistros { get; set; } = new List<BackupRegistro>();

    }
}
