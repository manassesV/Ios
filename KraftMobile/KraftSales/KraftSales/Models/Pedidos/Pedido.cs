using System;
using System.Collections.Generic;

namespace KraftSales.Models.Pedidos
{
    public class Pedido
    {
        public Pedido()
        {
            PedidoId = Guid.NewGuid().ToString();
            Itens = new List<ItemPedido>();
            DataPedido = DateTime.Now;
            Representante = "99999999";
            TipoPagamentoId = "0";
            RepresentanteInfo = new Pedidos.Representante { Codigo = "99999999", Nome = "MOCHINE", PercentualComissao = "0" };
            //Cliente = new 
        }

        public string PedidoId { get; set; }
        public DateTime DataEntrega { get; set; }
        public string TipoFrete { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public double ValorTotal { get; set; }
        public string TipoPagamentoId { get; set; }
        public string Representante { get; set; }
        public int Desconto { get; set; }
        public double ValorComDesconto { get; set; }
        public ICollection<ItemPedido> Itens { get; set; }
        public DateTime DataPedido { get; set; }
        public string DataPedidoFormatada => DataPedido.ToString("dd/MM/yyyy HH:mm");
        public string PedidoIdFormatado => PedidoId.Substring(0, 8).ToUpper();
        public string Observacao { get; set; }
        public Representante RepresentanteInfo { get; set; }
        public Database.Cliente Cliente { get; set; }
        public string Transporte { get; set; }
        public int TotalItens { get; set; }
        public bool PedidoDeClienteNovo { get; set; }
        public string NumeroPedido { get; set; }
        public string CnpjCliente { get; set; }
        public bool ClienteNaoCompraHaMaisDeDozeMeses { get; set; }
        public string ClienteTelefoneFormatado { get; set; }
    }
}
