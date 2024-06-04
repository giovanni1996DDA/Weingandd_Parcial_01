using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class VentaDoesNotExistException : Exception
    {
        public VentaDoesNotExistException() : base("La venta no existe")
        {

        }
    }
}
