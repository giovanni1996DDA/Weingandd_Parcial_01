using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class NoBoletosFoundException : Exception
    {
        public NoBoletosFoundException() : base("No se encontró ningun boleto con el criterio de búsqueda seleccionado")
        { 
        
        }
    }
}
