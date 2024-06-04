using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class ErrorDeletingException : Exception
    {
        public ErrorDeletingException() : base("No se han podido eliminar los elementos seleccionados")
        {
        
        } 
    }
}
