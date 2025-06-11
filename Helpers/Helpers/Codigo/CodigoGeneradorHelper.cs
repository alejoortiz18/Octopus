using Helpers.Interfaces;
using System.Numerics;

namespace Helpers.Codigo
{
    public class CodigoGeneradorHelperHelper : ICodigoHelper
    {
        public string GenerarCodigoUnico()
        {
            // Crear un nuevo GUID
            Guid guid = Guid.NewGuid();

            // Convertir los bytes del GUID a un número grande
            BigInteger bigInt = new BigInteger(guid.ToByteArray());

            // Asegurar que el número sea positivo
            if (bigInt < 0)
                bigInt = -bigInt;

            // Convertir el número a base 36 (0-9, A-Z)
            string base36 = Base36Encode(bigInt);

            // Tomar los primeros 8 caracteres
            return base36.Substring(0, 8).ToUpper();
        }

        private static string Base36Encode(BigInteger value)
        {
            const string caracteres = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string resultado = "";

            do
            {
                int index = (int)(value % 36);
                resultado = caracteres[index] + resultado;
                value /= 36;
            }
            while (value > 0);

            return resultado;
        }
    }
}
