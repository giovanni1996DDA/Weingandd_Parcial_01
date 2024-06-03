using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BoletoTurista : Boleto
    {
        public BoletoTurista(DateTime fechaSalida) : base(fechaSalida)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }

        public BoletoTurista(DateTime fechaSalida, int duracionViaje) : base(fechaSalida, duracionViaje)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }
    }
}
