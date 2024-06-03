using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class NoVentasFoundException : Exception
    {
        public NoVentasFoundException() : base("No se encontró ninguna venta con el criterio de búsqueda seleccionado") 
        { 
        }
    }
}
