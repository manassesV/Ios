using System.Collections.Generic;

namespace KraftSales.Models.PedidoDeVendas
{
    public class PedidoOrcamento
    {
        public PedidoOrcamento()
        {

        }

        public PedidoOrcamento(Models.Pedidos.Pedido order)
        {
            produtos = new List<ProdutoNoPedido>();

            if (order != null)
            {
                codigoPedido = order.PedidoId;
                desconto = order.Desconto;
                entrega = order.DataEntrega.ToString("dd/MM/yyyy");
                frete = order.TipoFrete;
                dataEmissao = order.DataPedido.ToString("dd/MM/yyyy");
                codigoVendedor = order.RepresentanteInfo.Codigo;
                perComissao = order.RepresentanteInfo.PercentualComissao;
                totalPecas = order.Itens.Count;
                clienteProspect = new ClienteProspect(order.Cliente);
                tipoPagamento = order.TipoPagamentoId;
                observacao = order.Observacao ?? string.Empty;
                transporte = order.Transporte ?? string.Empty;

                foreach (var item in order.Itens)
                {
                    produtos.Add(new ProdutoNoPedido { produto = new ProdutoInfo(item.CodigoProduto, item.Quantidade, item.Descricao) });
                }
            }

        }

        public string codigoPedido { get; set; }
        public double desconto { get; set; }
        public string entrega { get; set; }
        public string frete { get; set; }
        public string dataEmissao { get; set; }
        public string codigoVendedor { get; set; }
        public string perComissao { get; set; }
        public int totalPecas { get; set; }
        public ClienteProspect clienteProspect { get; set; }
        public string tipoPagamento { get; set; }
        public string observacao { get; set; }
        public string transporte { get; set; }
        public List<ProdutoNoPedido> produtos { get; set; }
    }


    public class ClienteProspect
    {
        public string clienteTipo { get; set; }
        public string clienteRazaoSocial { get; set; }
        public string clienteNomeFantasia { get; set; }
        public string clienteContato { get; set; }
        public string clienteEmail { get; set; }
        public string clienteTelefone { get; set; }
        public string clienteCNPJ { get; set; }
        public string clienteEndereco { get; set; }
        public string clienteEnderecoNro { get; set; }
        public string clienteEnderecoCpl { get; set; }
        public string clienteBairro { get; set; }
        public string clienteCidade { get; set; }
        public string clienteUF { get; set; }
        public string clienteCEP { get; set; }
        public string clienteDDD { get; set; }
        public string clienteInscEstadual { get; set; }

        public ClienteProspect()
        {

        }

        public ClienteProspect(Database.Cliente cliente)
        {
            if (cliente != null)
            {
                clienteTipo = "Prospect";
                clienteRazaoSocial = cliente.CliNom ?? string.Empty;
                clienteNomeFantasia = cliente.CliNomRdz ?? string.Empty;
                clienteContato = cliente.CliCntCom ?? string.Empty;
                clienteEmail = cliente.CliEndEle ?? string.Empty;
                clienteTelefone = cliente.CliTelCom ?? string.Empty;
                clienteCNPJ = cliente.CliCgc;
                clienteEndereco = cliente.CliEndFatSep ?? string.Empty;
                clienteEnderecoNro = cliente.NroEndFat ?? string.Empty;
                clienteEnderecoCpl = cliente.ComplEndFat ?? string.Empty;
                clienteBairro = cliente.CliBaiFat ?? string.Empty;
                clienteCidade = cliente.CliCidFat ?? string.Empty;
                clienteUF = cliente.CliEstFat ?? string.Empty;
                clienteCEP = clienteCEP ?? string.Empty;
                clienteDDD = cliente.CliDddCom ?? string.Empty;
                clienteInscEstadual = cliente.CliInsFat ?? string.Empty;
            }
        }
    }

}
