using DAO.Exceptions;
using DAO.Interfaces;
using Domain;
using Services.Facade;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAO.Implementations.Memory
{
    public sealed class VentaDao : IVentaDao
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
        /// <summary>
        /// Agrega una venta en la BBDD con un numero de ID unico autoincremental
        /// </summary>
        /// <param name="obj">Boleto a agregar</param>
        /// <returns>Retorna el id de la venta creada</returns>
        public int Add(Venta obj)
        {

            obj.id = _Venta.Any() ? _Venta.Max(b => b.id) + 1 : 0;
            _Venta.Add(obj);

            LoggerService.WriteLog($"Se agregó la venta {obj.id}", TraceLevel.Info);

            return obj.id;
        }

        public List<Venta> GetAll()
        {
            return (_Venta);
        }

        public Venta GetById(int id)
        {
            return _Venta.FirstOrDefault(b => b.id == id);
        }

        public int Update(Venta venta)
        {
            Venta ventaToUpdate = _Venta.FirstOrDefault(b => b.id == venta.id) ?? throw new VentaDoesNotExistException();

            ventaToUpdate.fechaVenta = venta.fechaVenta;

            LoggerService.WriteLog($"Se actualizó la venta {venta.id}", TraceLevel.Info);

            return venta.id;
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
