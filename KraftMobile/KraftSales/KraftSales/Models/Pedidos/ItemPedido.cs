using KraftSales.Models.Database;
using System;

namespace KraftSales.Models.Pedidos
{
    public class ItemPedido
    {
        public ItemPedido()
        {
            ItemPedidoId = Guid.NewGuid().ToString();
        }

        public string ItemPedidoId { get; set; }
        public string PedidoId { get; set; }
        public string TipoItem { get; set; }
        public int Quantidade { get; set; }
        public string ProdutoId { get; set; }
        public string CodigoProduto { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEscolhida { get; set; }
        public string Descricao { get; set; }
    }
}
