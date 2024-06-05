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

namespace Logic
{
    public class VentaLogic : IVentaLogic
    {
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

        public Venta GetByID(int id)
        {
            Venta venta;

            try
            {
                venta = FactoryDao.VentaDao.GetById(id);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            if (venta == null)
                throw new VentaDoesNotExistException();

            return venta;
        }
        public int Save(Venta venta)
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

        public int Update(Venta venta)
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

        public double getTotalVenta(Venta venta)
        {
            return venta.BoletosVendidos.Sum(b => BoletoLogic.Instance.getCostoBoleto(b));
        }
    }
}
