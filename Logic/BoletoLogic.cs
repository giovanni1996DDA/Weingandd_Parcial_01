using DAO.Implementations.Memory;
using Domain;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        //constructor privado para que no se instancie
        BoletoLogic() { }
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
                updateBoleto(obj);
            }
            else
            {
                addBoleto(obj);
            }
        }
        public DateTime CalcularRegreso(Boleto boleto)
        {
            if (boleto.TiempoEnDias == null)
                throw new UnespecifiedDurationTimeException();

            return boleto.FechaSalida.AddDays((double)boleto.TiempoEnDias);
        }

        public double getCostoBoleto(Boleto boleto)
        {

            //Se comenta el acceso al appsetings por error:
            //System.IO.FileNotFoundException: 
            //'Could not load file or assembly 'System.Configuration.ConfigurationManager, 
            //Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51'. 
            //The system cannot find the file specified.'

            if (boleto.tipoBoleto == (int) TipoBoleto.Ejecutivo)
                //return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["ExecutiveTravelPrice"]);
                return boleto.costoEmbarque + 9800;

            if (boleto.tipoBoleto == (int)TipoBoleto.Turista)
                //return boleto.costoEmbarque + Double.Parse(ConfigurationManager.AppSettings["TouristTravelPrice"]);
                return boleto.costoEmbarque + 8400;

            return boleto.costoEmbarque;
        }
        public string BoletoToString(Boleto boleto) 
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Boleto {boleto.destino.ToString()}");
            sb.AppendLine($"Fecha salida: {boleto.FechaSalida.ToString("d")}");
            sb.AppendLine($"Precio: {getCostoBoleto(boleto)}");
            sb.AppendLine($"Fecha regreso: {CalcularRegreso(boleto):d}");

            return sb.ToString();
        }
        private void addBoleto(Boleto boleto)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            addAndUpdateValidations(boleto);

            try
            {
                boletoDao.Add(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void updateBoleto(Boleto boleto)
        {
            BoletoDao boletoDao = BoletoDao.Instance;

            bool wasUpdated;

            addAndUpdateValidations(boleto);

            try
            {
                wasUpdated = boletoDao.Update(boleto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!wasUpdated)
                throw new ErrorUpdatingException();
        }

        private void addAndUpdateValidations(Boleto boleto)
        {
            if (boleto.FechaSalida == null)
                throw new NoDepartureDateException(boleto.destino);

            if (boleto.FechaSalida < DateTime.Today)
                throw new ExpiredDepartureDateException(boleto.destino);
        }

    }
}
