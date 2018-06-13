using System.Collections.Generic;
using System.Threading.Tasks;

namespace KraftSales.Services.Produtos
{
    public interface IProdutoSoapService
    {
        Task<List<Models.Database.Produto>> GetAllOnlineAsync(string dataUltimaAlteracao);
        Task<int> GetBalanceAvailable(string codigoProduto);
    }
}
