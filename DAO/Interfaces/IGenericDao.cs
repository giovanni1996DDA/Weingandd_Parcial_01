using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IGenericDao<T>
    {
        int Add(T obj);
        int Update(T obj);
        bool Remove(T obj);
        bool Exists(T obj);
        List<T> GetAll();
    }
}
