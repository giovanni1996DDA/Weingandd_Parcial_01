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
                throw new VentaEnCursoAlreadyExistsException();

            ventaEnCurso = new Venta();
        }
        public void CargarVenta(int idVenta)
        {
            try
            {
                ventaEnCurso = VentaLogic.Instance.GetByNumber(idVenta);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AgregarBoleto(string destino, DateTime fechaSalida, int? duracion = null, TipoBoleto tipoBoleto = TipoBoleto.Base)
        {
            
            //En duda de si las validaciones deberían ir acá o en la capa de logica de tipo de boleto.

            if (fechaSalida == null)
                throw new NoDepartureDateException();

            if (fechaSalida < DateTime.Today)
                throw new ExpiredDepartureDateException();


            switch (tipoBoleto) 
            {
                case TipoBoleto.Turista:
                    ventaEnCurso.BoletosVendidos.Add(new BoletoTurista(){
                                                                            NumeroEnVenta = ventaEnCurso.BoletosVendidos.Count + 1,
                                                                            destino = destino,
                                                                            FechaSalida = fechaSalida,
                                                                            TiempoEnDias = duracion
                                                                        });
                    break;

                case TipoBoleto.Ejecutivo:
                    ventaEnCurso.BoletosVendidos.Add(new BoletoEjecutivo(){
                                                                            NumeroEnVenta = ventaEnCurso.BoletosVendidos.Count + 1,
                                                                            destino = destino,
                                                                            FechaSalida = fechaSalida,
                                                                            TiempoEnDias = duracion
                                                                        });
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        public void EliminarBoletos(int nroEnVenta) 
        {

            Boleto boletoABorrar = ventaEnCurso.BoletosVendidos.FirstOrDefault(b => b.NumeroEnVenta == nroEnVenta) 
                                   ?? throw new BoletoEnVentaDoesNotExistException();

            bool wasDeleted = ventaEnCurso.BoletosVendidos.Remove(boletoABorrar);

            if (!wasDeleted)
                throw new ErrorDeletingException();

            //Reorganizo los numeros en venta
            for (int i = 0; i < ventaEnCurso.BoletosVendidos.Count; i++)
            {
                ventaEnCurso.BoletosVendidos[i].NumeroEnVenta = i + 1;
            }

        }
        public string ListarBoletos()
        {

            if (ventaEnCurso.BoletosVendidos.Count == 0)
                throw new VentaEnCursoHasNoBoletoException();

            StringBuilder sb = new StringBuilder();

            foreach (Boleto boleto in ventaEnCurso.BoletosVendidos) 
            {
                sb.AppendLine("-------------------------------");
                sb.AppendLine();
                sb.AppendLine(BoletoLogic.Instance.BoletoToString(boleto));
            }
            sb.AppendLine("-------------------------------");

            return sb.ToString();
            
        }
        public int CerrarVenta() 
        {
            if (VentaLogic.Instance.VentaExists(ventaEnCurso))
                BoletoLogic.Instance.DeleteBySaleID(ventaEnCurso.id);

            try
            {
                Guid idVenta = VentaLogic.Instance.SaveOrUpdate(ventaEnCurso);

                foreach (Boleto boleto in ventaEnCurso.BoletosVendidos)
                {
                    boleto.IDVenta = idVenta;
                    BoletoLogic.Instance.SaveOrUpdate(boleto);
                }
                //Chequear si al ponerlo null tambien me lo nullea en BBDD al ser un puntero. si lo hace, crear new venta en dao
                
                int nroVentaCerrada = ventaEnCurso.NroVenta;
                
                ventaEnCurso = null;

                return nroVentaCerrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double ObtenerPrecioBoleto(int NumeroEnVenta) 
        {
            Boleto boleto = ventaEnCurso.BoletosVendidos.FirstOrDefault(b => b.NumeroEnVenta == NumeroEnVenta) 
                                                                            ?? throw new BoletoEnVentaDoesNotExistException();

            return BoletoLogic.Instance.getCostoBoleto(boleto);
        }

        public double ObtenerTotalVentaEnCurso()
        {
            if (ventaEnCurso.BoletosVendidos.Count == 0)
                throw new VentaEnCursoHasNoBoletoException();

            return VentaLogic.Instance.getTotalVenta(ventaEnCurso);
        }
    }
}
