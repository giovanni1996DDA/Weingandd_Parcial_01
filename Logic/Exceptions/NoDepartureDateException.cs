using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class NoDepartureDateException : Exception
    {
        public NoDepartureDateException(string destino) : base ($"El boleto con destino a {destino} no posee fecha de salida.") 
        {

        }
    }
}
