using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class VentaEnCursoException : Exception
    {
        public VentaEnCursoException() : base("Ya existe una venta en curso")
        {
            
        }
    }
}
