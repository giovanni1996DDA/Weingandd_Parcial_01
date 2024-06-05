using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Exceptions
{
    public class BoletoDoesNotExistException : Exception
    {
        public BoletoDoesNotExistException() : base("El boleto que se esta intentando actualizar no existe.")
        { }
    }
}
