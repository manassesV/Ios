using KraftSales.Services.Pedidos;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.Pedidos.EnvioPedidoDroidService))]
namespace KraftSales.Droid.Services.Pedidos
{
    public class EnvioPedidoDroidService : IEnvioPedidoService
    {
        wsconsert.wsConsert ws;

        public EnvioPedidoDroidService()
        {
            ws = new wsconsert.wsConsert();
        }

        public async Task<Models.PedidoDeVendas.RetornoPedidoDeVenda> SendOrder(string jsonPedidoVenda)
        {
            if (!string.IsNullOrWhiteSpace(jsonPedidoVenda))
            {
                var result = ws.EnviarPedido(jsonPedidoVenda);

                if (string.IsNullOrWhiteSpace(result))
                {
                    return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
                }
                else
                {
                    var ret = JsonConvert.DeserializeObject<Models.PedidoDeVendas.RetornoPedidoDeVenda>(result);
                    return ret;
                }
            }

            return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
        }
    }

}