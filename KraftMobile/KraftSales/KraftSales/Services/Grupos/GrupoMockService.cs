using KraftSales.Models.Produtos;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KraftSales.Services.Grupos
{
    public class GrupoMockService : IGrupoService
    {
        private readonly ObservableCollection<Grupo> _mockGrupos = new ObservableCollection<Grupo>
        {
            new Grupo{ Codigo = "21", Descricao = "BERMUDA FEM"},
            new Grupo{ Codigo = "8", Descricao = "BERMUDA MASC"},
            new Grupo{ Codigo = "3", Descricao = "BLUSA FEM"},
            new Grupo{ Codigo = "11", Descricao = "BLUSA MASC"},
        };

        public async Task<ObservableCollection<Grupo>> GetAllGroupsAsync()
        {
            await Task.Delay(500);
            return _mockGrupos;
        }

        public async Task<Grupo> GetGrupoByIdAsync(string id)
        {
            await Task.Delay(200);
            return _mockGrupos.SingleOrDefault(c => c.Codigo == id);
        }
    }
}
