using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    internal interface IGenericLogic<T>
    {
        void SaveOrUpdate(T obj);
        void Delete(T obj);
        List<T> GetAll(); 
    }
}
