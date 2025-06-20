using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Banco
{
    public class BancoDto
    {
        public int BancoId { get; set; }
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public bool Activo { get; set; }
    }
}
