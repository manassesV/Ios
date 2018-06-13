using KraftSales.Models.Produtos;
using KraftSales.Models.Usuarios;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Produtos
{
    public interface IProdutoService
    {
        Task<ObservableCollection<Produto>> GetAllProductsMockAsync();
        Task<Produto> GetProdutoByIdMockAsync(string id);
        Task<Produto> GetProdutoByCodigoMockAsync(string codigo);

        Task<ObservableCollection<Models.Database.Produto>> GetAllOnlineAsync();
        Task<ObservableCollection<Models.Database.Produto>> GetAllOfflineAsync();
        Task<int> GetBalanceAvailable(string codigoProduto);
        Task<ObservableCollection<string>> GetAllCodesOfflineAsync();
        Task<Models.Database.Produto> GetProdutoAsync(string codigo);
        Task<bool> UpsertProdutosAsync(List<Models.Database.Produto> produtos);
        Task<int> GetProdutosOfflineCount();
        Task<bool> UpsertImagemProdutosAsync(List<GridReturn> imagens);
        Task<string> GetImagemProdutoAsync(string codigoProduto);
        Task<bool> UpsertGradeProdutosAsync(List<GridReturn> imagens);
        Task<ObservableCollection<Models.Database.GradeProduto>> GetGradeProduto (string codigoProduto);
    }
}
