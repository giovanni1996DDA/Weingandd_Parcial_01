using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class EmptyVentasException : Exception
    {
        public EmptyVentasException() : base("Las ventas deben tener asignado almenos un boleto")
        {

        }
    }
}
