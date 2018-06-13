using KraftSales.Models.Usuarios;
using System.Threading.Tasks;

namespace KraftSales.Services.Usuarios
{
    public interface IUsuarioService
    {
        Task<Usuario> AuthUser(string login, string password);
    }
}
