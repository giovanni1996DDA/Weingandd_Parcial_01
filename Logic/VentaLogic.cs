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
            VentaDao ventaDao = VentaDao.Instance;

            bool wasDeleted = false;

            try
            {
                wasDeleted = ventaDao.Remove(deleteVenta);
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
            VentaDao ventaDao = VentaDao.Instance;

            List<Venta> ventas = new List<Venta>();

            try
            {
                ventas = ventaDao.GetAll();
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
            VentaDao ventaDao = VentaDao.Instance;

            Venta venta;

            try
            {
                venta = ventaDao.GetById(id);

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

        public void SaveOrUpdate(Venta obj)
        {
            VentaDao ventaDao = VentaDao.Instance;

            if (ventaDao.Exists(obj))
            {
                updateVenta(obj);
            }
            else
            {
                addVenta(obj);
            }
        }

        private void addVenta(Venta venta)
        {
            VentaDao ventaDao = VentaDao.Instance;

            //Debo tener al menos un boleto
            if (venta.BoletosVendidos.Count < 1)
                throw new EmptyVentasException();

            try
            {
                ventaDao.Add(venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void updateVenta(Venta venta)
        {
            VentaDao ventaDao = VentaDao.Instance;

            try
            {
                ventaDao.Update(venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
