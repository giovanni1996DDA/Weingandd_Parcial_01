using System;
using System.Text;

namespace Domain
{
    public abstract class Boleto
    {

        public readonly double costoEmbarque = 9950.0;

        public int tipoBoleto;
        public DateTime FechaSalida { get; set; }
        public int Numero { get; set; }
        public int TiempoEnDias { get; set; }

        //Si el boleto es solo de ida, no hace falta defnir el tiempo de duracion del viaje
        public Boleto(DateTime fechaSalida)
        {
            this.FechaSalida = fechaSalida;
        }
        public Boleto(DateTime fechaSalida, int tiempoDias)
        {
            this.FechaSalida = fechaSalida;
            this.TiempoEnDias = tiempoDias;
        }
        //public abstract double getCostoBoleto();

        /*public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Boleto {Numero.ToString()}");
            sb.AppendLine($"Fecha salida: {FechaSalida.ToString("d")}");
            sb.AppendLine($"Precio: {getCostoBoleto()}");
            sb.AppendLine($"Fecha regreso: {CalcularRegreso():d}");

            return sb.ToString();
        }*/
    }
    public enum TipoBoleto 
    { 
        Base,
        Turista,
        Ejecutivo
    }
}
