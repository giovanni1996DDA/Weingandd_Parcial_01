using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Venta
    {
        public Guid id { get; set; }
        public int NroVenta { get; set; }
        public DateTime fechaVenta { get; set; }
        public List<Boleto> BoletosVendidos { get; set; }
        public Venta(DateTime? fechaDeVenta = null)
        {
            id = Guid.NewGuid();
            fechaVenta = fechaDeVenta ?? DateTime.Today;
            BoletosVendidos = new List<Boleto>();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Venta nro: {NroVenta}\n");
            sb.Append($"Fecha de creación: {fechaVenta:d}");
            
            return sb.ToString();
        }
    }
}
