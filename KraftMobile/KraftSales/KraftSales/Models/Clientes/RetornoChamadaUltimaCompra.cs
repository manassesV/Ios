namespace KraftSales.Models.Clientes
{
    public class RetornoChamadaUltimaCompra
    {
        public UltimaCompra UltimaCompra { get; set; }
    }

    public class UltimaCompra
    {
        public string CliCgc { get; set; }
        public string retorno { get; set; }
    }
}



