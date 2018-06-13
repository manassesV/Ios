namespace KraftSales.Models.Pedidos
{
    public class TipoFrete
    {
        //public const string CIF = "CIF";
        //public const string FOB = "FOB";
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
