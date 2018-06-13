using KraftSales.Models.Impressoras;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Impressoras
{
    public interface IImpressoraService
    {
        Task<ObservableCollection<Impressora>> GetPrintersAsync();
    }
}
