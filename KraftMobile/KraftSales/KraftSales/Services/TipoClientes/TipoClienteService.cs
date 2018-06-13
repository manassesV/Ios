using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoClientes
{
    public class TipoClienteService : ITipoClienteService
    {
        private ObservableCollection<TipoCliente> _mockItems = new ObservableCollection<TipoCliente>
        {
            new TipoCliente{ Codigo = "0", Descricao = "Pessoa Jurídica"},
            new TipoCliente{ Codigo = "1", Descricao = "Pessoa Física"},
        };

        public async Task<ObservableCollection<TipoCliente>> GetAllAsync()
        {
            return await Task.FromResult(_mockItems);
        }
    }
}
