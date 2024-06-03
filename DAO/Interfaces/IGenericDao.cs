using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IGenericDao<T>
    {
        void Add(T obj);
        bool Update(T obj);
        bool Remove(T obj);
        bool Exists(T obj);
        List<T> GetAll();
    }
}
