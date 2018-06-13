using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.TipoFretes
{
    public interface ITipoFreteService
    {
        Task<ObservableCollection<TipoFrete>> GetAllAsync();
    }
}
