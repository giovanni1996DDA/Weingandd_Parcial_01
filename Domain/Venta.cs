using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Venta
    {
        public int id { get; set; }
        public DateTime fechaVenta { get; set; }
        public List<Boleto> BoletosVendidos { get; set; }

        public Venta(DateTime fechaDeVenta) 
        {
            fechaVenta = fechaDeVenta;
        }
    }
}
