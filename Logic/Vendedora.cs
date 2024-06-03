using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Vendedora
    {
        #region singleton
        private readonly static Vendedora _instance = new Vendedora();

        public static Vendedora Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion
        public Vendedora() 
        {
            
        }
    }
}
