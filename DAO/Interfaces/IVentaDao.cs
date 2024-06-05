using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IVentaDao : IGenericDao<Venta>
    {
        Venta GetByNumber(int id);
    }
}
