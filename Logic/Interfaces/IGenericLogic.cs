using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    internal interface IGenericLogic<T>
    {
        int Save(T obj);
        int Update(T obj);
        void Delete(T obj);
        List<T> GetAll(); 
    }
}
