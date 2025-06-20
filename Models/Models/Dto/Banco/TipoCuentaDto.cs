using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Banco
{
    public class TipoCuentaDto
    {
        public string Nombre { get; set; }
        public List<DetalleTipoCuenta> Tipocuenta { get; set; } = new List<DetalleTipoCuenta>();

    }
}
