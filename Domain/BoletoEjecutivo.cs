using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BoletoEjecutivo : Boleto
    {
        public BoletoEjecutivo(DateTime fechaSalida) : base(fechaSalida)
        {
            tipoBoleto = (int) TipoBoleto.Ejecutivo;
        }

        public BoletoEjecutivo(DateTime fechaSalida, int duracionViaje) : base(fechaSalida, duracionViaje)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }
    }
}
