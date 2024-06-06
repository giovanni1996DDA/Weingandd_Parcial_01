using DAO.Exceptions;
using DAO.Factory;
using DAO.Interfaces;
using Domain;
using Services.Facade;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DAO.Implementations.Memory
{
    public sealed class BoletoDao : IBoletoDao
    {
        private static List<Boleto> _Boletos = new List<Boleto>();

        #region singleton
        private readonly static BoletoDao _instance = new BoletoDao();

        public static BoletoDao Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion
        /// <summary>
        /// Agrega un boleto en la BBDD con un número de NumeroEnVenta autoincremental en base a la venta
        /// </summary>
        /// <param name="obj">Boleto a agregar</param>
        public Guid Add(Boleto obj)
        {

            //List<Boleto> boletosEnVenta = _Boletos.Where(b => b.IDVenta == obj.IDVenta).ToList();

            //obj.NumeroEnVenta = boletosEnVenta.Any() ? boletosEnVenta.Max(b => b.NumeroEnVenta) + 1 : 0;

            _Boletos.Add(obj);

            LoggerService.WriteLog($"Se agregó el boleto {obj.ID}", TraceLevel.Info);

            return obj.ID;
        }
        /// <summary>
        /// Obtiene todos los boletos en la BBDD
        /// </summary>
        /// <returns></returns>
        public List<Boleto> GetAll()
        {
            return (_Boletos);
        }
        /// <summary>
        /// Obtiene los boletos por el numero
        /// </summary>
        /// <param name="NumeroBoleto">Numero de boleto a buscar</param>
        /// <returns></returns>
        public Boleto GetById(Guid id)
        {
            return _Boletos.FirstOrDefault(b => b.ID == id) ?? throw new BoletoDoesNotExistException();
        }
        /// <summary>
        /// Obtiene todos los boletos de una venta en especifica
        /// </summary>
        /// <param name="venta">La venta de referencia para buscar los boletos</param>
        /// <returns></returns>
        public List<Boleto> GetBySale(Guid idVenta)
        {
            return _Boletos.Where(b => b.IDVenta == idVenta).ToList() ?? throw new BoletoDoesNotExistForSaleException();
        }
        /// <summary>
        /// Actualiza el boleto enviado
        /// </summary>
        /// <param name="boleto">Boleto a actualizar</param>
        /// <returns></returns>
        public Guid Update(Boleto boleto)
        {
            Boleto boletoToUpdate = _Boletos.FirstOrDefault(b => b.ID == boleto.ID) ?? throw new BoletoDoesNotExistException();

            boletoToUpdate.FechaSalida = boleto.FechaSalida;
            boletoToUpdate.TiempoEnDias = boleto.TiempoEnDias;

            LoggerService.WriteLog($"Se actualizó el boleto {boleto.ID}", TraceLevel.Info);

            return boleto.ID;
        }
        /// <summary>
        /// Elimina el voleto enviado
        /// </summary>
        /// <param name="removeBoleto">Boleto a eliminar</param>
        /// <returns></returns>
        public bool Remove(Boleto removeBoleto)
        {
            LoggerService.WriteLog($"Se eliminó el boleto {removeBoleto.ID}", TraceLevel.Info);
            return _Boletos.Remove(_Boletos.FirstOrDefault(b => b.ID == removeBoleto.ID));
        }

        public bool Exists(Boleto boleto)
        {
            return _Boletos.Any(b => b.ID == boleto.ID); 
        }

        public void DeleteBySaleID(Guid id)
        {
            int rowsAffected = _Boletos.RemoveAll(b => b.IDVenta == id);

            LoggerService.WriteLog($"Se eliminaron {rowsAffected} boletos de la BBDD en base al id de compra: {id}", TraceLevel.Info);
        }
    }
}
