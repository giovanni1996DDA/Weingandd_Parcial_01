using System;
using System.Text;

namespace Domain
{
    public abstract class Boleto
    {

        public readonly double costoEmbarque = 9950.0;

        public int tipoBoleto;
        public string Destino { get; set; }
        public DateTime FechaSalida { get; set; }
        public Guid ID { get; set; }
        public int NumeroEnVenta { get; set; }
        public Guid IDVenta { get; set; }
        public int? TiempoEnDias { get; set; }

        //Si el boleto es solo de ida, no hace falta defnir el tiempo de duracion del viaje
        public Boleto(string destino, DateTime? fechaSalida)
        {
            ID  = Guid.NewGuid();
            this.FechaSalida = fechaSalida ?? DateTime.Now;
        }
        public Boleto(string destino, DateTime? fechaSalida, int tiempoDias)
        {
            ID = Guid.NewGuid();
            this.FechaSalida = fechaSalida ?? DateTime.Now;
            this.TiempoEnDias = tiempoDias;
        }

        protected Boleto()
        {
            ID = Guid.NewGuid();
        }
        //public abstract double getCostoBoleto();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Boleto: {NumeroEnVenta} \n");
            sb.Append($"Destino: {Destino} \n");
            sb.Append($"Fecha salida: {FechaSalida:d}\n");

            return sb.ToString();
        }
    }
    public enum TipoBoleto 
    { 
        Base,
        Turista,
        Ejecutivo
    }
}
