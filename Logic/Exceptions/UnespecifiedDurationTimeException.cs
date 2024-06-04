using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class UnespecifiedDurationTimeException : Exception
    {
        public UnespecifiedDurationTimeException() : base("No se puede estimar una fecha de regreso si no hay una duracion estimada") 
        {
        
        }
    }
}
