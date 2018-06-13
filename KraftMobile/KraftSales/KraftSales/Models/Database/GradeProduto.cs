using Realms;

namespace KraftSales.Models.Database
{
    public class GradeProduto : RealmObject
    {
        public string CodigoProduto { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public int Quantidade { get; set; }
    }
}
