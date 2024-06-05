using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class VentaEnCursoHasNoBoletoException : Exception
    {
        public VentaEnCursoHasNoBoletoException() : base("La venta no posee ningun boleto asignado.") 
        { 
        
        }
    }
}
