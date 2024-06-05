using DAO.Exceptions;
using DAO.Interfaces;
using Domain;
using Services.Facade;
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
        /// Agrega un boleto en la BBDD con un numero de ID unico autoincremental
        /// </summary>
        /// <param name="obj">Boleto a agregar</param>
        public int Add(Boleto obj)
        {
            
            obj.Numero = _Boletos.Any() ? _Boletos.Max(b => b.Numero) + 1 : 0;
            _Boletos.Add(obj);

            LoggerService.WriteLog($"Se agregó el boleto {obj.Numero}", TraceLevel.Info);

            return obj.Numero;
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
        public Boleto GetByNumber(int NumeroBoleto)
        {
            return _Boletos.FirstOrDefault(b => b.Numero == NumeroBoleto);
        }
        /// <summary>
        /// Obtiene todos los boletos de una venta en especifica
        /// </summary>
        /// <param name="venta">La venta de referencia para buscar los boletos</param>
        /// <returns></returns>
        //public  List<Boleto> GetBySale(Venta venta)
        //{

        //}
        /// <summary>
        /// Actualiza el boleto enviado
        /// </summary>
        /// <param name="boleto">Boleto a actualizar</param>
        /// <returns></returns>
        public int Update(Boleto boleto)
        {
            Boleto boletoToUpdate = _Boletos.FirstOrDefault(b => b.Numero == boleto.Numero) ?? throw new BoletoDoesNotExistException();

            boletoToUpdate.FechaSalida = boleto.FechaSalida;
            boletoToUpdate.TiempoEnDias = boleto.TiempoEnDias;

            LoggerService.WriteLog($"Se actualizó el boleto {boleto.Numero}", TraceLevel.Info);

            return boleto.Numero;
        }
        /// <summary>
        /// Elimina el voleto enviado
        /// </summary>
        /// <param name="removeBoleto">Boleto a eliminar</param>
        /// <returns></returns>
        public bool Remove(Boleto removeBoleto)
        {
            LoggerService.WriteLog($"Se eliminó el boleto {removeBoleto.Numero}", TraceLevel.Info);
            return _Boletos.Remove(_Boletos.FirstOrDefault(b => b.Numero == removeBoleto.Numero));
        }

        public bool Exists(Boleto obj)
        {
            return _Boletos.Any(b => b.Numero == obj.Numero); 
        }
    }
}
