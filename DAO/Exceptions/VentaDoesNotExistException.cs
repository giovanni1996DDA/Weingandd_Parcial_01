using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Exceptions
{
    internal class VentaDoesNotExistException : Exception
    {
        public VentaDoesNotExistException() : base("La venta que se esta intentando actualizar no existe.") { }
    }
}
