using KraftSales.Models.Pedidos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Pedidos
{
    public interface IPedidoService
    {
        Task<ObservableCollection<Pedido>> GetOrdersToApproveAsync();
        Task<Models.PedidoDeVendas.RetornoPedidoDeVenda> SendOrder(Pedido pedido);
    }
}
