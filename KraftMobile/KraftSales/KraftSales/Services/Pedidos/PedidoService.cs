using KraftSales.Extensions;
using KraftSales.Models.Pedidos;
using KraftSales.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KraftSales.Services.Pedidos
{
    public class PedidoService : BaseService, IPedidoService
    {
        private List<Pedido> _mockOrdersToApprove = new List<Pedido>
        {
            new Pedido { Representante = "5", RepresentanteInfo = new Representante{ Codigo = "5", Nome = "ALEXANDRE PECANHA LATTARI"}, DataPedido = DateTime.Now.AddMinutes(-30), Desconto = 20, ValorTotal = 499.90, DataEntrega = DateTime.Now.AddDays(4), TipoFrete = "CIF", Observacao = "Cliente é legal", TipoPagamentoId = "0",
                Itens = new List<ItemPedido> {
                    new ItemPedido{ CodigoProduto = "P01", Quantidade = 10, Valor = 499.90, TipoItem = "Pack",}
                }, },
            new Pedido { Representante = "65", RepresentanteInfo = new Representante{ Codigo = "65", Nome = "AMARILDO LORENZETTI"}, DataPedido = DateTime.Now.AddMinutes(-20), Desconto = 25, ValorTotal = 599.80, DataEntrega = DateTime.Now.AddDays(6), TipoFrete = "FOB", Observacao = "Cliente é bacana", TipoPagamentoId = "30/60/90",
                Itens = new List<ItemPedido> {
                    new ItemPedido{ CodigoProduto = "P02", Quantidade = 20, Valor = 599.80, TipoItem = "Caixa",}
                },  },
            new Pedido { Representante = "56", RepresentanteInfo = new Representante{ Codigo = "56", Nome = "GUTO E OLIVEIRA"}, DataPedido = DateTime.Now.AddMinutes(-5), Desconto = 30, ValorTotal = 1199.7, DataEntrega = DateTime.Now.AddDays(8), TipoFrete = "CIF", Observacao = "Cliente compra bastante", TipoPagamentoId = "30/60/90/120",
                Itens = new List<ItemPedido> {
                    new ItemPedido{ CodigoProduto = "P03", Quantidade = 30, Valor = 1199.7, TipoItem = "Unidade",}
                },  },
        };

        private readonly IEnvioPedidoService envioPedidoService;

        public PedidoService() : base("")
        {
            envioPedidoService = DependencyService.Get<IEnvioPedidoService>();
        }

        public async Task<ObservableCollection<Pedido>> GetOrdersToApproveAsync()
        {
            await Task.Delay(500);
            return _mockOrdersToApprove.OrderBy(c => c.DataPedido).ToObservableCollection();
        }

        public async Task<Models.PedidoDeVendas.RetornoPedidoDeVenda> SendOrder(Pedido pedido)
        {
            if (pedido != null)
            {
                try
                {
                    var pedidoVenda = new Models.PedidoDeVendas.ChamadaPedidoDeVenda(pedido);
                    var jsonPedidoVenda = JsonConvert.SerializeObject(pedidoVenda);

                    if (pedido.PedidoDeClienteNovo)
                    {
                        //return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Função de pedido para clientes novos ainda não está disponível"));

                        var result = await PostToWebApi<Models.PedidoDeVendas.RetornoPedidoOrcamento>($"ge1G82s9lv/android/quote", pedidoVenda);
                        if (result != null)
                        {
                            return new Models.PedidoDeVendas.RetornoPedidoDeVenda(result.pedidoOrcamento);
                        }
                        else
                        {
                            return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
                        }
                    }
                    else
                    {
                        return await envioPedidoService.SendOrder(jsonPedidoVenda);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
                }
            }

            return await Task.FromResult(new Models.PedidoDeVendas.RetornoPedidoDeVenda("Erro ao enviar pedido"));
        }
    }
}
