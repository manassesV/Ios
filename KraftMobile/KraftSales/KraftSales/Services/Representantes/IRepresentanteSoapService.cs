using KraftSales.Models.Pedidos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KraftSales.Services.Representantes
{
    public interface IRepresentanteSoapService
    {
        Task<List<Representante>> GetAllOnlineAsync();
    }
}
