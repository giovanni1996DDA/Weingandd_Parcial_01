using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BoletoEjecutivo : Boleto
    {
        public BoletoEjecutivo() : base()
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }

        public BoletoEjecutivo(string destino, DateTime fechaSalida) : base(destino, fechaSalida)
        {
            tipoBoleto = (int) TipoBoleto.Ejecutivo;
        }

        public BoletoEjecutivo(string destino, DateTime fechaSalida, int duracionViaje) : base(destino, fechaSalida, duracionViaje)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }
    }
}
