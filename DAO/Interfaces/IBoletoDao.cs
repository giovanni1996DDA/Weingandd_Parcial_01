using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAO.Interfaces
{
    public interface IBoletoDao : IGenericDao<Boleto>
    {
        void DeleteBySaleID(Guid id);
        List<Boleto> GetBySale(Guid idVenta);
    }
}
