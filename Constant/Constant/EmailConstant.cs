using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public static class EmailConstant
    {
        #region RESTABLECER CONTRASEÑA

        public const string AsuntoRestablecerContrasena = "Restablecer Contraseña";

        public const string CuerpoRestablecerContrasena = $@"
                    <p>Estimado usuario,</p>
                    <p>Hemos recibido una solicitud para restablecer la contraseña de su cuenta en <strong>Octopus</strong>.</p>
                    <p>Para completar el proceso, utilice el siguiente código:</p>
                    <h2 style='color: #2E86C1;'>{{codigo}}</h2>
                    <p><strong>Este código es de un solo uso y tiene una validez limitada.</strong></p>
                    <p>Si no solicitó este cambio, ignore este mensaje.</p>
                    <p></p>
                    <p>Atentamente,</p>
                    <p><strong>Equipo de Octopus</strong></p>";

        #endregion

        #region CREAR USUARIO
        public const string AsuntoUsuarioNuevo = "Bienvenid@ a Octopus";

        public const string CuerpoCreateUsuario = $@"
                    <p>Estimado usuario,</p>
                    <p>Bienvenid@ a <strong>Octopus</strong>.</p>
                    <p>Para completar el proceso de registro, utilice el siguiente código:</p>
                    <h2 style='color: #2E86C1;'>{{codigo}}</h2>
                    <p><strong>Este código es de un solo uso y tiene una validez limitada.</strong></p>
                    <p>Si no solicitó este cambio, ignore este mensaje.</p>
                    <p></p>
                    <p>Atentamente,</p>
                    <p><strong>Equipo de Octopus</strong></p>                    
                    <p><a href='https://localhost:7121/EnableUser/ValidarCodigo?codigo={{codigo}}&email={{email}}' 
                        style='font-weight: bold; font-size: 14pt;'>Restablecer contraseña</a></p>
                    </p>                    
                    ";
        #endregion
    }
}
