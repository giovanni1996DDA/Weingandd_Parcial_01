using DAO.Interfaces;
using Domain;
using Services.Facade;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void Add(Boleto obj)
        {
            obj.Numero = _Boletos.Max(b => b.Numero) + 1;
            _Boletos.Add(obj);

            LoggerService.WriteLog($"Se agregó el boleto {obj.Numero}", TraceLevel.Info);
        }

        public List<Boleto> GetAll()
        {
            return (_Boletos);
        }

        public Boleto GetByNumber(int NumeroBoleto)
        {
            return _Boletos.FirstOrDefault(b => b.Numero == NumeroBoleto);
        }

        public bool Update(Boleto boleto)
        {
            Boleto boletoToUpdate = _Boletos.FirstOrDefault(b => b.Numero == boleto.Numero);

            if (boletoToUpdate == null)
                return false;

            boletoToUpdate.FechaSalida = boleto.FechaSalida;
            boletoToUpdate.TiempoEnDias = boleto.TiempoEnDias;

            LoggerService.WriteLog($"Se actualizó el boleto {boleto.Numero}", TraceLevel.Info);

            return true;
        }

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
