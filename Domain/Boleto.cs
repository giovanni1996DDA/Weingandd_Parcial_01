﻿using System;
using System.Text;

namespace Domain
{
    public abstract class Boleto
    {

        public readonly double costoEmbarque = 9950.0;

        public int tipoBoleto;
        public string destino { get; set; }
        public DateTime FechaSalida { get; set; }
        public int Numero { get; set; }
        public int NumeroEnVenta { get; set; }
        public int IDVenta { get; set; }
        public int? TiempoEnDias { get; set; }

        //Si el boleto es solo de ida, no hace falta defnir el tiempo de duracion del viaje
        public Boleto(string destino, DateTime? fechaSalida)
        {
            this.FechaSalida = fechaSalida ?? DateTime.Now;
        }
        public Boleto(string destino, DateTime? fechaSalida, int tiempoDias)
        {
            this.FechaSalida = fechaSalida ?? DateTime.Now;
            this.TiempoEnDias = tiempoDias;
        }

        protected Boleto()
        {
        }
        //public abstract double getCostoBoleto();

        /*public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Boleto {Numero.ToString()}");
            sb.AppendLine($"Fecha salida: {FechaSalida.ToString("d")}");
            //sb.AppendLine($"Precio: {getCostoBoleto()}");
            //sb.AppendLine($"Fecha regreso: {CalcularRegreso():d}");

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
