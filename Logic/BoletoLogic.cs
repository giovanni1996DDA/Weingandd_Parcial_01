using DAO.Implementations.Memory;
using Domain;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class BoletoLogic
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
        #endregion

        public void Delete(Boleto deleteboleto)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            bool wasDeleted = false;

            try
            {
                wasDeleted = boletoDao.Remove(deleteboleto);
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
            BoletoDao boletoDao = BoletoDao.Instance;

            List<Boleto> boletos = new List<Boleto>();

            try
            {
                boletos = boletoDao.GetAll();
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
            BoletoDao boletoDao = BoletoDao.Instance;

            Boleto boleto;

            try
            {
                boleto = boletoDao.GetByNumber(number);

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

        public void SaveOrUpdate(Boleto obj)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            if (boletoDao.Exists(obj))
            {
                addBoleto(obj);
            }
            else
            {
                updateBoleto(obj);
            }
        }

        private void addBoleto(Boleto boleto)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            try
            {
                boletoDao.Update(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void updateBoleto(Boleto boleto)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            try
            {
                boletoDao.Update(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime CalcularRegreso(Boleto boleto)
        {
            if (boleto.TiempoEnDias <= 1)
                throw new UnespecifiedDurationTimeException();

            return boleto.FechaSalida.AddDays(boleto.TiempoEnDias);
        }

        public double getCostoBoleto(Boleto boleto)
        {
            if (boleto.tipoBoleto == (int) TipoBoleto.Ejecutivo)
                return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["ExecutiveTravelPrice"]);

            if (boleto.tipoBoleto == (int)TipoBoleto.Turista)
                return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["TouristTravelPrice"]);

            return boleto.costoEmbarque;
        }
    }
}
