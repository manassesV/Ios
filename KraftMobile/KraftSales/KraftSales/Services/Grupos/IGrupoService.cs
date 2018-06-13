using KraftSales.Models.Produtos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Grupos
{
    public interface IGrupoService
    {
        Task<ObservableCollection<Grupo>> GetAllGroupsAsync();
        Task<Grupo> GetGrupoByIdAsync(string id);
    }
}
