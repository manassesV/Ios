using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoPagamentos
{
    public class TipoPagamentoService : ITipoPagamentoService
    {
        private ObservableCollection<TipoPagamento> _mockItems = new ObservableCollection<TipoPagamento>
        {
            new TipoPagamento{ Codigo = "0", Descricao = "A vista"},
            new TipoPagamento{ Codigo = "30/60", Descricao = "30/60"},
            new TipoPagamento{ Codigo = "30/45/60", Descricao = "30/45/60"},
            new TipoPagamento{ Codigo = "30/60/90", Descricao = "30/60/90"},
            new TipoPagamento{ Codigo = "30/60/90/120", Descricao = "30/60/90/120"},
            new TipoPagamento{ Codigo = "30/45/60/75/90", Descricao = "30/45/60/75/90"},
            //new TipoPagamento{ Codigo = "30/60/90/120/150", Descricao = "30/60/90/120/150"},
            new TipoPagamento{ Codigo = "30/45/60/75/90/105/120", Descricao = "30/45/60/75/90/105/120"},
            new TipoPagamento{ Codigo = "30/45/60/75/90/105/120/135", Descricao = "30/45/60/75/90/105/120/135"},
            new TipoPagamento{ Codigo = "45/60/75", Descricao = "45/60/75"},
        };

        public async Task<ObservableCollection<TipoPagamento>> GetAllAsync()
        {
            return await Task.FromResult(_mockItems);
        }
    }
}
