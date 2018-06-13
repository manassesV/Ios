using System.Collections.Generic;

namespace KraftSales.Models.PedidoDeVendas
{
    public class ChamadaPedidoDeVenda
    {
        public PedidoDeVenda pedido { get; set; }

        public ChamadaPedidoDeVenda(Pedidos.Pedido order)
        {
            pedido = new PedidoDeVenda(order);
        }
    }

    public class PedidoDeVenda
    {
        public PedidoDeVenda()
        {

        }

        public PedidoDeVenda(Pedidos.Pedido order)
        {
            produtos = new List<ProdutoNoPedido>();

            if (order != null)
            {
                codigoPedido = order.PedidoId;
                desconto = order.Desconto + ".00";
                entrega = order.DataEntrega.ToString("dd/MM/yyyy");
                frete = order.TipoFrete;
                dataEmissao = order.DataPedido.ToString("dd/MM/yyyy");
                codigoVendedor = order.RepresentanteInfo.Codigo;
                perComissao = order.RepresentanteInfo.PercentualComissao;
                totalPecas = order.TotalItens;
                cliente = new Cliente(order.Cliente);
                //tipoPagamento = order.TipoPagamentoId;
                pagamento = new PagamentoInfo(order.TipoPagamentoId);
                observacao = order.Observacao ?? string.Empty;
                transportadora = order.Transporte ?? string.Empty;
                observacao += $" {transportadora}";
                transportadora = string.Empty;

                if (order.ClienteNaoCompraHaMaisDeDozeMeses)
                {
                    observacao += $" Cliente não compra há mais de 12 meses";
                }

                foreach (var item in order.Itens)
                {
                    produtos.Add(new ProdutoNoPedido { produto = new ProdutoInfo(item.CodigoProduto, item.Quantidade, item.Descricao) });
                }
            }

        }

        public string codigoPedido { get; set; }
        public string desconto { get; set; }
        public string entrega { get; set; }
        public string frete { get; set; }
        public string transportadora { get; set; }
        public string dataEmissao { get; set; }
        public string codigoVendedor { get; set; }
        public string perComissao { get; set; }
        public int totalPecas { get; set; }
        public string observacao { get; set; }
        public Cliente cliente { get; set; }
        public PagamentoInfo pagamento { get; set; }
        public List<ProdutoNoPedido> produtos { get; set; }
    }

    public class PagamentoInfo
    {
        public string descricao { get; set; }
        public string tipo { get; set; }

        public PagamentoInfo(string tipoPagamentoId)
        {
            descricao = tipoPagamentoId;
            tipo = tipoPagamentoId;
        }
    }

    public class Cliente
    {
        //public string clienteTipo { get; set; }
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

        public Cliente()
        {

        }

        public Cliente(Database.Cliente cliente)
        {
            if (cliente != null)
            {
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

    public class ProdutoNoPedido
    {
        public ProdutoInfo produto { get; set; }
    }

    public class ProdutoInfo
    {
        public string codigoProduto { get; set; }
        //public int quantidade { get; set; }
        public string cor { get; set; } = string.Empty;
        public string descricao { get; set; } = string.Empty;
        public Grade grade { get; set; }

        public ProdutoInfo(string codigoProdutoProduto, int quantidade, string descricaoProduto)
        {
            codigoProduto = codigoProdutoProduto;
            descricao = descricaoProduto;
            cor = "Única";
            grade = new Grade { caixa = 0, pack = 0, unidade = quantidade };
        }
    }

    public class Grade
    {
        public int caixa { get; set; }
        public int pack { get; set; }
        public int unidade { get; set; }
    }

}
