using KraftSales.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KraftSales.Services.Clientes
{
    public interface IClienteSoapService
    {
        Task<List<Cliente>> GetAllOnlineAsync(string dataUltimaAlteracao);
        Task<Models.Clientes.RetornoChamadaUltimaCompra> GetLastBuyDate(string cnpj);
    }
}
