namespace KraftSales.Models.Pedidos
{
    public class TipoPagamento
    {
        //public string TipoPagamentoId { get; set; }
        //public string Codigo { get; set; }
        //public string Descricao { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
