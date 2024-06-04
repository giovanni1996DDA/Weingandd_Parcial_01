using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BoletoTurista : Boleto
    {
        public BoletoTurista() : base()
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }

        public BoletoTurista(string destino, DateTime fechaSalida) : base(destino, fechaSalida)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }

        public BoletoTurista(string destino, DateTime fechaSalida, int duracionViaje) : base(destino, fechaSalida, duracionViaje)
        {
            tipoBoleto = (int)TipoBoleto.Ejecutivo;
        }
    }
}
