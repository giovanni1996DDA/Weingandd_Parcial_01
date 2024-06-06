using DAO.Implementations.Memory;
using DAO.Interfaces;
using Domain;
using Logic.Exceptions;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Factory;
using DAO.Exceptions;

namespace Logic
{
    public class VentaLogic : IGenericLogic<Venta>
    {
        private List<Venta> _VentaCache = new List<Venta>();
        #region singleton
        private readonly static VentaLogic _instance = new VentaLogic();

        public static VentaLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        //constructor privado para que no se instancie
        VentaLogic() { }
        #endregion

        public void Delete(Venta deleteVenta)
        {
            bool wasDeleted = false;

            try
            {
                wasDeleted = FactoryDao.VentaDao.Remove(deleteVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!wasDeleted)
                throw new ErrorDeletingException();
        }
        public List<Venta> GetAll()
        {
            List<Venta> ventas = new List<Venta>();

            try
            {
                ventas = FactoryDao.VentaDao.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (ventas.Count() == 0)
                throw new NoVentasFoundException();

            return ventas;
        }
        public Venta GetByID(Guid id)
        {
            try
            {
                Venta venta = FactoryDao.VentaDao.GetById(id);
                venta.BoletosVendidos = BoletoLogic.Instance.GetBySaleID(venta.id);
                return venta;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Venta GetByNumber(int numeroVenta)
        {
            try
            {
                Venta venta = FactoryDao.VentaDao.GetByNumber(numeroVenta);
                venta.BoletosVendidos = BoletoLogic.Instance.GetBySaleID(venta.id);
                return venta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveOrUpdate(Venta venta)
        {
            try
            {
                if (VentaExists(venta))
                {
                    Update(venta);
                }
                else
                {
                    Save(venta);
                }
                return venta.id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Guid Save(Venta venta)
        {
            //Debo tener al menos un boleto
            if (venta.BoletosVendidos.Count < 1)
                throw new EmptyVentasException();

            try
            {
                return FactoryDao.VentaDao.Add(venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Guid Update(Venta venta)
        {
            try
            {
                return FactoryDao.VentaDao.Update(venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double GetTotalVenta(Venta venta)
        {
            return venta.BoletosVendidos.Sum(b => BoletoLogic.Instance.getCostoBoleto(b));
        }
        public bool VentaExists(Venta venta)
        {
            return FactoryDao.VentaDao.Exists(venta);
        }
    }
}
