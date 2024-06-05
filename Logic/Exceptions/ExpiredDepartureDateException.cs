using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class ExpiredDepartureDateException : Exception
    {
        public ExpiredDepartureDateException() : base($"La fecha de salida del boleto se encuentra en el pasado.") 
        {

        }
    }
}
