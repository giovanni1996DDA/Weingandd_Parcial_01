using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class NoVentasFoundException : Exception
    {
        public NoVentasFoundException() : base("No se encontró ninguna compra.") 
        { 
        }
    }
}
