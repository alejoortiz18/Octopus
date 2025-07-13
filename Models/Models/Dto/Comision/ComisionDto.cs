using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Comision
{
    public class ComisionDto
    {
        public int Nivel { get; set; }
        public int UsuarioID { get; set; }
        public string NombreCompleto { get; set; }
        public int Porcentaje { get; set; }
        public bool PagoUnico { get; set; } = true;
    }
}
