using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoClientes
{
    public interface ITipoClienteService
    {
        Task<ObservableCollection<TipoCliente>> GetAllAsync();
    }
}
