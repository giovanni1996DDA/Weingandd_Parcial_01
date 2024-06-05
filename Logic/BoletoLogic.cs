using DAO.Factory;
using DAO.Implementations.Memory;
using DAO.Interfaces;
using Domain;
using Logic.Exceptions;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    internal class BoletoLogic : IGenericLogic<Boleto>
    {
        #region singleton
        private readonly static BoletoLogic _instance = new BoletoLogic();

        public static BoletoLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        //constructor privado para que no se instancie
        BoletoLogic() { }
        #endregion

        public void Delete(Boleto deleteboleto)
        {
            bool wasDeleted = false;

            try
            {
                wasDeleted = FactoryDao.BoletoDao.Remove(deleteboleto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!wasDeleted)
                throw new ErrorDeletingException();
        }
        public List<Boleto> GetAll()
        {
            List<Boleto> boletos = new List<Boleto>();

            try
            {
                boletos = FactoryDao.BoletoDao.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (boletos.Count() == 0)
                throw new NoBoletosFoundException();

            return boletos;
        }

        public Boleto GetByID(int number)
        {
            Boleto boleto;

            try
            {
                boleto = FactoryDao.BoletoDao.GetByNumber(number);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            if (boleto == null)
                throw new BoletoDoesNotExistException();

            return boleto;
        }
        public DateTime CalcularRegreso(Boleto boleto)
        {
            if (boleto.TiempoEnDias == null)
                throw new UnespecifiedDurationTimeException();

            return boleto.FechaSalida.AddDays((double)boleto.TiempoEnDias);
        }

        public double getCostoBoleto(Boleto boleto)
        {

            if (boleto.tipoBoleto == (int)TipoBoleto.Ejecutivo)
                return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["ExecutiveTravelPrice"]);

            if (boleto.tipoBoleto == (int)TipoBoleto.Turista)
                return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["TouristTravelPrice"]);

            return boleto.costoEmbarque;
        }
        public string BoletoToString(Boleto boleto)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Boleto nro: {boleto.NumeroEnVenta}");
            sb.AppendLine($"Destino: {boleto.destino}");
            sb.AppendLine($"Fecha salida: {boleto.FechaSalida:d}");
            sb.AppendLine($"Precio: {getCostoBoleto(boleto)}");
            sb.AppendLine($"Fecha regreso: {CalcularRegreso(boleto):d}");

            return sb.ToString();
        }
        public int Save(Boleto boleto)
        {
            try
            {
                return FactoryDao.BoletoDao.Add(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Boleto boleto)
        {
            try
            {
                return FactoryDao.BoletoDao.Update(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void addAndUpdateValidations(Boleto boleto)
        //{
        //    if (boleto.FechaSalida == null)
        //        throw new NoDepartureDateException(boleto.destino);

        //    if (boleto.FechaSalida < DateTime.Today)
        //        throw new ExpiredDepartureDateException(boleto.destino);
        //}

    }
}
