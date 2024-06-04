using Domain;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Vendedora
    {
        private Venta ventaEnCurso = null;

        #region singleton
        private readonly static Vendedora _instance = new Vendedora();

        public static Vendedora Instance
        {
            get
            {
                return _instance;
            }
        }
        Vendedora()
        {

        }
        #endregion

        public void AbrirVenta() 
        {
            if (ventaEnCurso != null)
                throw new VentaEnCursoException();

            ventaEnCurso = new Venta();
        }
        public void AgregarBoleto(string destino, DateTime fechaSalida, int? duracion = null, TipoBoleto tipoBoleto = TipoBoleto.Base)
        {
            /*
            En duda de si las validaciones deberían ir acá o en la capa de logica de tipo de boleto.

            if (fechaSalida == null)
                throw new NoDepartureDateException(destino);

            if (fechaSalida < DateTime.Today)
                throw new ExpiredDepartureDateException(destino);
            */

            switch (tipoBoleto) 
            {
                case TipoBoleto.Turista:
                    ventaEnCurso.BoletosVendidos.Add(new BoletoTurista(){
                                                                            destino = destino,
                                                                            FechaSalida = fechaSalida,
                                                                            TiempoEnDias = duracion
                                                                        });
                    break;

                case TipoBoleto.Ejecutivo:
                    ventaEnCurso.BoletosVendidos.Add(new BoletoEjecutivo(){
                                                                            destino = destino,
                                                                            FechaSalida = fechaSalida,
                                                                            TiempoEnDias = duracion
                                                                        });
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        public void EliminarBoletos(string destino) 
        {
            ventaEnCurso.BoletosVendidos.RemoveAll(b => b.destino == destino);
        }
        public string ListarBoletos()
        {
            BoletoLogic bllBoleto = BoletoLogic.Instance;

            StringBuilder sb = new StringBuilder();

            foreach (Boleto boleto in ventaEnCurso.BoletosVendidos) 
            {
                sb.AppendLine(bllBoleto.BoletoToString(boleto));
                sb.AppendLine("-------------------------------");
            }

            return sb.ToString();
            
        }
        public void CerrarVenta() 
        {
            BoletoLogic bllBoleto = BoletoLogic.Instance;
            VentaLogic  bllVenta  = VentaLogic.Instance;

            try
            {
                foreach (Boleto boleto in ventaEnCurso.BoletosVendidos)
                {
                    bllBoleto.SaveOrUpdate(boleto);
                }

                bllVenta.SaveOrUpdate(ventaEnCurso);

                //Chequear si al ponerlo null tambien me lo nullea en BBDD al ser un puntero. si lo hace, crear new venta en dao
                ventaEnCurso = null;
            }
            catch (EmptyVentasException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
