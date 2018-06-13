using KraftSales.Services.Pedidos;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.iOS.Services.Pedidos.EnvioPedidoiOSService))]
namespace KraftSales.iOS.Services.Pedidos
{
    public class EnvioPedidoiOSService : IEnvioPedidoService
    {
        wsconsert.wsConsert ws;

        public EnvioPedidoiOSService()
        {
            ws = new wsconsert.wsConsert();
        }

        public async Task<Models.PedidoDeVendas.RetornoPedidoDeVenda> SendOrder(string jsonPedidoVenda)
        {
            if (!string.IsNullOrWhiteSpace(jsonPedidoVenda))
            {
                //try
                //{
                //    if (pedido.PedidoDeClienteNovo)
                //    {
                //        var pedidoOrcamento = new Models.PedidoDeVendas.PedidoOrcamento(pedido);
                //        var jsonPedidoOrcamento = JsonConvert.SerializeObject(pedidoOrcamento);

                //        return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Função de pedido para clientes novos ainda não está disponível"));
                //    }
                //    else
                //    {
                //        var pedidoVenda = new Models.PedidoDeVendas.ChamadaPedidoDeVenda(pedido);
                //        var jsonPedidoVenda = JsonConvert.SerializeObject(pedidoVenda);

                //        //var result = await GetFromWebApi<Models.PedidoDeVendas.RetornoPedidoDeVenda>($"EnviarPedido?json={jsonPedidoVenda}");
                //        //var result = await PostToWebApi<Models.PedidoDeVendas.RetornoPedidoDeVenda>($"EnviarPedido", jsonPedidoVenda);

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
            //}
            //catch (Exception)
            //{
            //    return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
            //}
            //}

            return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
        }
    }
}