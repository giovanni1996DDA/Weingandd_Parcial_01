using DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Factory
{
    public static class FactoryDao
    {
        private static int backendType;
        static FactoryDao()
        {
            backendType = int.Parse(ConfigurationManager.AppSettings["BackendType"]);
        }

        public static IBoletoDao BoletoDao
        {
            get
            {
                if (backendType == (int)BackendType.Memory)
                    return DAO.Implementations.Memory.BoletoDao.Instance;
                else
                    throw new NotImplementedException();
            }
        }
    }

    internal enum BackendType
    {
        Memory,
        SqlServer,
    }
}
