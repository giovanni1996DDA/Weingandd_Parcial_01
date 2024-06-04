using Domain;
using Logic;
using System.Globalization;

namespace Console_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderOpts();

            int optHeader = int.Parse(Console.ReadLine());

            Vendedora catalina = Vendedora.Instance;

            while (optHeader != (int)HeaderOpts.Salir)
            {
                catalina.AbrirVenta();
                Console.WriteLine("");
                Console.WriteLine("Se ha abierto una compra.");
                Console.WriteLine("");

                PrintFooterOpts();

                int optFooter = int.Parse(Console.ReadLine());

                while (optFooter != (int)FooterOpts.CerrarVenta)
                {
                    switch (optFooter)
                    {
                        case (int)FooterOpts.ListarBoletos:
                            ModuleListarBoletos(catalina);
                            break;
                        case (int)FooterOpts.AgregarBoleto:
                            ModuleAgregarBoleto(catalina);
                            Console.WriteLine("El boleto se agregó correctamente.");
                            break;
                        case (int)FooterOpts.ModificarBoleto:
                            Console.WriteLine("Lo siento, esta opción aún no se encuentra activa.");
                            break;
                        case (int)FooterOpts.EliminarBoletos:
                            ModuleEliminarBoletos(catalina);
                            break;
                        case (int)FooterOpts.PrecioBoleto:

                            break;
                        case (int)FooterOpts.TotalVenta:

                            break;
                        case (int)FooterOpts.CerrarVenta:

                            break;
                    }

                    PrintFooterOpts();

                    optFooter = int.Parse(Console.ReadLine());
                }
            }
        }

        private static void ModuleAgregarBoleto(Vendedora catalina)
        {
            Console.WriteLine("Elija un destino:");
            string destino = Console.ReadLine().ToUpper();

            Console.WriteLine("Por favor, ingrese una fecha (formato: dd/mm/yyyy):");
            DateTime fechaSalida = parseFecha();

            Console.WriteLine("Elija la duración del viaje, si  es que es un viaje de ida y vuelta. Si no lo es, ingrese 0.");
            int duracion = parseInt();

            Console.WriteLine("Elija un tipo de boleto:");
            Console.WriteLine($"{(int)TipoBoleto.Turista} - Turista");
            Console.WriteLine($"{(int)TipoBoleto.Ejecutivo} - Ejecutivo");
            TipoBoleto tipoBoleto = (TipoBoleto)int.Parse(Console.ReadLine());

            catalina.AgregarBoleto(destino, fechaSalida, duracion, tipoBoleto);
        }

        private static void ModuleEliminarBoletos(Vendedora catalina)
        {
            Console.WriteLine("Elija un destino (Tenga en cuenta que se eliminarán todos los boletos con el mismo destino. Elija 0 si desea cancelar.):");
            string destino = Console.ReadLine().ToUpper();

            if (destino == "0")
                return;

            catalina.EliminarBoletos(destino);
        }
        private static void ModuleListarBoletos(Vendedora catalina)
        {
            Console.WriteLine("A continuación, se listan los boletos activos:");
            Console.WriteLine("");
            Console.WriteLine(catalina.ListarBoletos());
        }

        private static void PrintHeaderOpts()
        {
            Console.WriteLine("");
            Console.WriteLine("Bienvenido al sistema de ventas de nekomata airlines. Mi nombre es Catalina.");
            Console.WriteLine("");
            Console.WriteLine("¿Que operación desea realizar?");
            Console.WriteLine("");
            Console.WriteLine($"{(int)HeaderOpts.CrearVenta} - Crear Compra");
            Console.WriteLine($"{(int)HeaderOpts.Salir} - Salir");
        }

        private static void PrintFooterOpts()
        {
            Console.WriteLine("¿Que operación desea realizar?");
            Console.WriteLine("");
            Console.WriteLine($"{(int)FooterOpts.ListarBoletos}  - Listar Boletos");
            Console.WriteLine($"{(int)FooterOpts.AgregarBoleto}  - Agregar Boleto");
            Console.WriteLine($"{(int)FooterOpts.ModificarBoleto}  - Modificar Boleto");
            Console.WriteLine($"{(int)FooterOpts.EliminarBoletos}  - Eliminar Boleto");
            Console.WriteLine($"{(int)FooterOpts.PrecioBoleto}  - Calcular precio de un Boleto");
            Console.WriteLine($"{(int)FooterOpts.TotalVenta}  - Calcular total de la Venta");
            Console.WriteLine($"{(int)FooterOpts.CerrarVenta}  - Cerrar Compra");
        }

        private static DateTime parseFecha()
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
        public static int parseInt()
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
        public enum HeaderOpts
        {
            CrearVenta  = 1,
            Salir       = 2,
        }
        public enum FooterOpts
        {
            ListarBoletos   = 1,
            AgregarBoleto   = 2,
            ModificarBoleto = 3,
            EliminarBoletos = 4,
            PrecioBoleto    = 5,
            TotalVenta      = 6,
            CerrarVenta     = 7,
        }
    }
}