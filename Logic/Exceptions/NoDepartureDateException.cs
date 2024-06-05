using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class NoDepartureDateException : Exception
    {
        public NoDepartureDateException() : base ($"El boleto debe tener fecha de salida.") 
        {

        }
    }
}
