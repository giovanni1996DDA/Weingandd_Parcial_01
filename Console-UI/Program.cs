using Console_UI.Helpers;
using Domain;
using Logic;
using Logic.Exceptions;
using System.Globalization;

namespace Console_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vendedora catalina = Vendedora.Instance;

            bool exitLoop = false;

            while (!exitLoop)
            {
                PrintHeaderOpts();
                HeaderOpts optHeader = NotNullableInput.notNullableInputEnum<HeaderOpts>();

                switch (optHeader) 
                {
                    case HeaderOpts.Crear_Compra:
                        ModuleCrearVenta(catalina);
                        break;
                    case HeaderOpts.Modificar_Compra:
                        ModuleModificarCompra(catalina);
                        break;
                    case HeaderOpts.Eliminar_Compra:
                        throw new NotImplementedException();
                    case HeaderOpts.Salir:
                        exitLoop = true;
                        break;
                }
            }

            Console.WriteLine("Gracias por confiar en Nekomata Airlines. Que tenga buen viaje!");
        }

        private static void ModuleModificarCompra(Vendedora catalina)
        {

            Console.WriteLine("");
            Console.WriteLine("Escriba el ID de la compra que desea modificar:");
            int id = NotNullableInput.notNullableInputInt();

            try
            {
                ModuleCargarVenta(catalina, id);

                ModuleFooter(catalina);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Desea realizar alguna otra operación?");
        }

        private static void ModuleCargarVenta(Vendedora catalina, int nroVenta)
        {
            try
            {
                catalina.CargarVenta(nroVenta);
                Console.WriteLine("");
                Console.WriteLine($"Se ha cargado la compra con número de venta: {nroVenta}");
                Console.WriteLine("");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ModuleCrearVenta(Vendedora catalina) 
        {
            try
            {
                ModuleAbrirVenta(catalina);

                ModuleFooter(catalina);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Desea realizar alguna otra operación?");

        }

        private static void ModuleAbrirVenta(Vendedora catalina)
        {
            try
            {
                catalina.AbrirVenta();
                Console.WriteLine("");
                Console.WriteLine("Se ha abierto una compra.");
                Console.WriteLine("");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ModuleFooter(Vendedora catalina)
        {
            bool exitLoop = false;

            //while (optFooter != FooterOpts.Cerrar_Venta && optFooter != FooterOpts.Salir_Sin_Guardar)
            while (!exitLoop)
            {
                PrintFooterOpts();

                FooterOpts optFooter = NotNullableInput.notNullableInputEnum<FooterOpts>();

                switch (optFooter)
                {
                    case FooterOpts.Listar_Boletos:
                        ModuleListarBoletos(catalina);
                        break;
                    case FooterOpts.Agregar_Boleto:
                        ModuleAgregarBoleto(catalina);
                        break;
                    case FooterOpts.Modificar_Boleto:
                        Console.WriteLine("Lo siento, esta opción aún no se encuentra activa.");
                        break;
                    case FooterOpts.Eliminar_Boletos:
                        ModuleEliminarBoletos(catalina);
                        break;
                    case FooterOpts.Obtener_Precio_Boleto:
                        ModuleObtenerPrecioBoleto(catalina);
                        break;
                    case FooterOpts.Obtener_Total_Venta:
                        ModuleObtenerTotalVenta(catalina);
                        break;
                    case FooterOpts.Cerrar_Venta:
                        ModuleCerrarVenta(catalina);
                        exitLoop = true;
                        break;
                    case FooterOpts.Salir_Sin_Guardar:
                        exitLoop = true;
                        break;
                }
            }
        }

        private static void ModuleCerrarVenta(Vendedora catalina)
        {
            try
            {
                int idVenta = catalina.CerrarVenta();

                Console.WriteLine($"Sus boletos han sido reservados correctamente con el número de venta {idVenta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ModuleObtenerTotalVenta(Vendedora catalina)
        {
            try
            {
                Console.WriteLine($"El total de la venta es: {catalina.ObtenerTotalVentaEnCurso()}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ModuleObtenerPrecioBoleto(Vendedora catalina)
        {
            Console.WriteLine("Seleccione el boleto del cual desea saber el precio. Elija 0 para salir:");
            Console.WriteLine();

            int nroBoletoEnVenta = NotNullableInput.notNullableInputInt();

            try
            {
                Console.WriteLine($"El precio del boleto es de ${catalina.ObtenerPrecioBoleto(nroBoletoEnVenta)}");
                Console.WriteLine();
            }
            catch (BoletoEnVentaDoesNotExistException)
            {
                Console.WriteLine($"El boleto {nroBoletoEnVenta} no existe. Reintente por favor.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ModuleAgregarBoleto(Vendedora catalina)
        {
            Console.WriteLine("Elija un destino:");
            string destino = NotNullableInput.notNullableInputString().ToUpper();

            Console.WriteLine();

            Console.WriteLine("Por favor, ingrese una fecha (formato: dd/mm/yyyy):");
            DateTime fechaSalida = NotNullableInput.notNullableInputDate();

            Console.WriteLine();

            Console.WriteLine("Elija la duración del viaje, si  es que es un viaje de ida y vuelta. Si no lo es, ingrese 0.");
            int duracion = NotNullableInput.notNullableInputInt();

            Console.WriteLine();

            Console.WriteLine("Elija un tipo de boleto:");
            Console.WriteLine($"{(int)TipoBoleto.Turista} - Turista");
            Console.WriteLine($"{(int)TipoBoleto.Ejecutivo} - Ejecutivo");
            TipoBoleto tipoBoleto = NotNullableInput.notNullableInputEnum<TipoBoleto>();
            Console.WriteLine();

            try
            {
                catalina.AgregarBoleto(destino, fechaSalida, duracion, tipoBoleto);
                Console.WriteLine("El boleto se agrego correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void ModuleEliminarBoletos(Vendedora catalina)
        {
            ModuleListarBoletos(catalina);

            Console.WriteLine("Elija el boleto a eliminar. Elija 0 para salir:");
            int nroBoletoEnVenta = NotNullableInput.notNullableInputInt();

            if (nroBoletoEnVenta == 0)
                return;

            Console.WriteLine();

            try
            {
                catalina.EliminarBoletos(nroBoletoEnVenta);
                Console.WriteLine("El boleto se eliminó correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        private static void ModuleListarBoletos(Vendedora catalina)
        {
            Console.WriteLine("A continuación, se listan los boletos activos:");
            Console.WriteLine();

            try
            {
                Console.WriteLine(catalina.ListarBoletos());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void PrintHeaderOpts()
        {
            Console.WriteLine("");
            Console.WriteLine("Bienvenido al sistema de ventas de nekomata airlines. Mi nombre es Catalina.");
            Console.WriteLine("");
            Console.WriteLine("¿Que operación desea realizar?");
            Console.WriteLine("");
            foreach(var value in Enum.GetValues(typeof(HeaderOpts)))
            {
                Console.WriteLine($"{(int)value} - {value}");
            }
        }
        private static void PrintFooterOpts()
        {
            Console.WriteLine("¿Que operación desea realizar?");
            Console.WriteLine("");
            foreach (var value in Enum.GetValues(typeof(FooterOpts)))
            {
                Console.WriteLine($"{(int)value} - {value}");
            }
        }
        public enum HeaderOpts
        {
            Crear_Compra        = 1,
            Modificar_Compra    = 2,
            Eliminar_Compra     = 3,
            Salir               = 4
        }
        public enum FooterOpts
        {
            Listar_Boletos          = 1,
            Agregar_Boleto          = 2,
            Modificar_Boleto        = 3,
            Eliminar_Boletos        = 4,
            Obtener_Precio_Boleto   = 5,
            Obtener_Total_Venta     = 6,
            Cerrar_Venta            = 7,
            Salir_Sin_Guardar       = 8
        }
    }
}