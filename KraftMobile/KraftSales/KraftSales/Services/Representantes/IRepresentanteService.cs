using KraftSales.Models.Pedidos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Representantes
{
    public interface IRepresentanteService
    {
        Task<ObservableCollection<Representante>> GetAllOnlineAsync();
        Task<ObservableCollection<Representante>> GetAllOfflineAsync();
        Task<Representante> GetAsync(string codigo);
        Task<bool> UpsertItemsAsync(List<Representante> representantes);
    }
}
