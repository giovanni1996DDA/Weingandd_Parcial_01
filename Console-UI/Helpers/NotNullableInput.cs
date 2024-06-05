using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Console_UI.Program;

namespace Console_UI.Helpers
{
    public static class NotNullableInput
    {
        public static DateTime notNullableInputDate()
        {
            DateTime fecha;
            while (true)
            {
                string entrada = Console.ReadLine();

                if (DateTime.TryParseExact(entrada, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Fecha no válida. Intente nuevamente.");
                }
            }
            return fecha;
        }
        public static int notNullableInputInt()
        {
            int n;
            while (true)
            {
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out n))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Número no válido. Intente nuevamente.");
                }
            }
            return n;
        }
        public static string notNullableInputString()
        {
            string entrada;

            while (true)
            {
                entrada = Console.ReadLine();

                if (!string.IsNullOrEmpty(entrada))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Debe seleccionar un destino. Intente nuevamente.");
                }
            }
            return entrada;
        }

        public static TEnum notNullableInputEnum<TEnum>() where TEnum : struct, Enum
        {
            while (true)
            {
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int valor) && Enum.IsDefined(typeof(TEnum), valor))
                {
                    return (TEnum)Enum.ToObject(typeof(TEnum), valor);
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                }
            }
        }
    }
}
