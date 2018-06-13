namespace KraftSales.Models.Pedidos
{
    public class TipoCliente
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
