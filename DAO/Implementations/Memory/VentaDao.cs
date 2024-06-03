using DAO.Interfaces;
using Domain;
using Services.Facade;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAO.Implementations.Memory
{
    public sealed class VentaDao : IVentaoDao
    {
        private static List<Venta> _Venta = new List<Venta>();

        #region singleton
        private readonly static VentaDao _instance = new VentaDao();

        public static VentaDao Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion
        public void Add(Venta obj)
        {
            obj.id = _Venta.Max(b => b.id) + 1;
            _Venta.Add(obj);

            LoggerService.WriteLog($"Se agregó la venta {obj.id}", TraceLevel.Info);
        }

        public List<Venta> GetAll()
        {
            return (_Venta);
        }

        public Venta GetById(int id)
        {
            return _Venta.FirstOrDefault(b => b.id == id);
        }

        public bool Update(Venta venta)
        {
            Venta ventaToUpdate = _Venta.FirstOrDefault(b => b.id == venta.id);

            if (ventaToUpdate == null)
                return false;

            ventaToUpdate.fechaVenta = venta.fechaVenta;

            LoggerService.WriteLog($"Se actualizó la venta {venta.id}", TraceLevel.Info);

            return true;
        }

        public bool Remove(Venta removeVenta)
        {
            bool wasDeleted = _Venta.Remove(_Venta.FirstOrDefault(b => b.id == removeVenta.id));

            if (!wasDeleted)
                return false;

            LoggerService.WriteLog($"Se eliminó la venta {removeVenta.id}", TraceLevel.Info);
            return wasDeleted;

        }

        public bool Exists(Venta obj)
        {
            return _Venta.Any(b => b.id == obj.id); ;
        }
    }
}
