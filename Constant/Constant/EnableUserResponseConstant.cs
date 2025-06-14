using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public static class EnableUserResponseConstant
    {
        public const string FechaExpiracionTokenMsn ="El código ha expirado. Por favor, solicite un nuevo código para continuar.";
        public const string CodigoAndEmailInvalidoMsn = "El código y el Correo son obligatorios.";
        public const string BloqueoUsuarioMsn = "El usuario esta bloqueado temporalmente.";
        public const string TokenInvalidoMsn = "El token es inválido.";
    }
}
