using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Exceptions
{
    internal class BoletoDoesNotExistForSaleException : Exception
    {
        public BoletoDoesNotExistForSaleException() : base("No existen boletos para la venta seleccionada")
        {

        }
    }
}
