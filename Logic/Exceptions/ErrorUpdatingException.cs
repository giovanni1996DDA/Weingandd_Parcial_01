using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class ErrorUpdatingException : Exception
    {
        public ErrorUpdatingException() : base("Hubo un error al actualizar")
        {

        }
    }
}
