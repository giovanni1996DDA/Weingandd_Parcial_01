using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class ExpiredDepartureDateException : Exception
    {
        public ExpiredDepartureDateException(string destino) : base($"Para el boleto con destino {destino}, la fecha de salida es anterior a la fecha de hoy") 
        {

        }
    }
}
