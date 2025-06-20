using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Banco
{
    public class TipoCuentaDto
    {
        [Display(Name = "Tipo de cuenta")]
        public string Nombre { get; set; }
        public List<DetalleTipoCuenta> Tipocuenta { get; set; } = new List<DetalleTipoCuenta>();
    }
}
