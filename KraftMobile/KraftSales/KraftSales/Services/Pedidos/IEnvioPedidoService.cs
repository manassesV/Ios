using System.Threading.Tasks;

namespace KraftSales.Services.Pedidos
{
    public interface IEnvioPedidoService
    {
        Task<Models.PedidoDeVendas.RetornoPedidoDeVenda> SendOrder(string jsonPedidoVenda);
    }
}
