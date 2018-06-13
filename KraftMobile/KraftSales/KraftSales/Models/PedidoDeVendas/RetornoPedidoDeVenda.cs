namespace KraftSales.Models.PedidoDeVendas
{
    public class RetornoPedidoDeVenda
    {
        public Pedido pedido { get; set; }

        public RetornoPedidoDeVenda()
        {

        }

        public RetornoPedidoDeVenda(Pedidoorcamento pedidoOrcamento)
        {
            if (pedidoOrcamento != null)
            {
                pedido = new Pedido { codigoMK = pedidoOrcamento.codigoPortalDkp, codigo = pedidoOrcamento.codigo, dataCadastroMK = pedidoOrcamento.dataCadastroPortalDkp, retorno = pedidoOrcamento.retorno };
            }

        }

        public RetornoPedidoDeVenda(string msgErro)
        {
            pedido = new Pedido { codigoMK = "000000", retorno = msgErro };
        }
    }

    public class Pedido
    {
        public string codigo { get; set; }
        public string codigoMK { get; set; }
        public string dataCadastroMK { get; set; }
        public string retorno { get; set; }
    }

    public class RetornoPedidoOrcamento
    {
        public Pedidoorcamento pedidoOrcamento { get; set; }

        public RetornoPedidoOrcamento()
        {

        }
    }

    public class Pedidoorcamento
    {
        public string codigo { get; set; }
        public string codigoPortalDkp { get; set; }
        public string dataCadastroPortalDkp { get; set; }
        public string retorno { get; set; }
    }

}




