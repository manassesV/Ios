using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoPagamentos
{
    public interface ITipoPagamentoService
    {
        Task<ObservableCollection<TipoPagamento>> GetAllAsync();
    }
}
