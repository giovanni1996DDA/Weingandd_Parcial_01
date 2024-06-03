using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class BoletoDoesNotExistException : Exception
    {
        public BoletoDoesNotExistException() : base("El boleto no existe")
        {

        }
    }
}
