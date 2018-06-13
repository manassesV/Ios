using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoFretes
{
    public class TipoFreteService : ITipoFreteService
    {
        private ObservableCollection<TipoFrete> _mockItems = new ObservableCollection<TipoFrete>
        {
            new TipoFrete{ Codigo = "CIF", Descricao = "CIF"},
            new TipoFrete{ Codigo = "FOB", Descricao = "FOB"},
        };

        public async Task<ObservableCollection<TipoFrete>> GetAllAsync()
        {
            return await Task.FromResult(_mockItems);
        }
    }
}
