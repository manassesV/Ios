using KraftSales.Models.Pedidos;
using System.Linq;
using System.Text;

namespace KraftSales.Helpers
{
    public class MailHelper
    {
        public static string GenerateMailBody(Pedido pedido)
        {
            if (pedido == null)
            {
                return string.Empty;
            }

            var texto = new StringBuilder();
            string tipoPedido = pedido.PedidoDeClienteNovo ? "Orçamento" : "Pedido";
            string tipoPagamento = pedido.TipoPagamentoId == "0" ? "A VISTA" : pedido.TipoPagamentoId;
            texto.Append($"<b><u>{tipoPedido} de Venda MOCHINE - Via DKP Fair</u></b><br/><br/>");
            texto.Append($"Numero: {pedido.NumeroPedido ?? string.Empty}<br/><br/>");
            texto.Append($"<b><u>Cliente</u></b><br/>");
            texto.Append($"CNPJ: {pedido.CnpjCliente}<br/>");
            texto.Append($"Razao Social: {pedido.Cliente.CliNom}<br/>");
            texto.Append($"Nome Fantasia: {pedido.Cliente.CliNomRdz}<br/>");
            if (!string.IsNullOrWhiteSpace(pedido.Cliente.CliDddCom))
            {
                texto.Append($"Telefone: ({pedido.Cliente.CliDddCom}) {pedido.Cliente.CliTelCom}<br/>"); 
            }
            else
            {
                texto.Append($"Telefone: {pedido.Cliente.CliTelCom}<br/>");
            }
            texto.Append($"E-mail: {pedido.Cliente.CliEndEle}<br/><br/>");
            texto.Append($"<b><u>Dados pedido</u></b><br/>");
            texto.Append($"Representante: {pedido.RepresentanteInfo.Nome}<br/>");
            texto.Append($"Data estimada entrega: {pedido.DataEntrega.ToString("dd/MM/yyyy")}<br/>");
            texto.Append($"Frete: {pedido.TipoFrete}<br/>");
            texto.Append($"Transportadora: {pedido.Transporte}<br/>");
            texto.Append($"Observacao: {pedido.Observacao}<br/><br/>");
            texto.Append($"<b><u>Itens do Pedido</u></b><br/>");
            texto.Append($"<table>");
            texto.Append($"<tr>");
            texto.Append($"<td align='center'><u>Codigo</u></td>");
            texto.Append($"<td align='center'><u>Descricao</u></td>");
            texto.Append($"<td align='center'><u>Quantidade</u></td>");
            texto.Append($"<td align='center'><u>Valor (R$)</u></td>");
            texto.Append($"</tr>");

            foreach (var item in pedido.Itens.OrderBy(C => C.CodigoProduto).ToList())
            {
                texto.Append($"<tr>");
                texto.Append($"<td align='center'>{item.CodigoProduto}</td>");
                texto.Append($"<td align='center'>{item.Descricao}</td>");
                texto.Append($"<td align='center'>{item.Quantidade}</td>");
                texto.Append($"<td align='center'>{item.Valor.ToString("F")}</td>");
                texto.Append($"</tr>");
            }

            texto.Append($"</table>");
            texto.Append($"<br/><br/>");
            texto.Append($"Total Itens: {pedido.TotalItens}<br/>");
            texto.Append($"Valor Total: R$ {pedido.ValorTotal.ToString("F")}<br/>");
            texto.Append($"Desconto: {pedido.Desconto.ToString("F")}% <br/>");
            texto.Append($"Valor Com Desconto: R$ {pedido.ValorComDesconto.ToString("F")}<br/>");

            texto.Append($"Tipo Pagamento: {tipoPagamento}<br/>");

            return texto.ToString();
        }
    }
}
