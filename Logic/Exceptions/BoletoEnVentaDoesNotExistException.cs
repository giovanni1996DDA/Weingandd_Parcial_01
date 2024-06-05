using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class BoletoEnVentaDoesNotExistException : Exception
    {
        public BoletoEnVentaDoesNotExistException() : base("El boleto que se esta intentando eliminar no existe.") 
        {
            
        }
    }
}
