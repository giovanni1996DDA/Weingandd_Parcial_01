﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    internal class VentaEnCursoAlreadyExistsException : Exception
    {
        public VentaEnCursoAlreadyExistsException() : base("Ya existe una venta en curso")
        {
            
        }
    }
}
