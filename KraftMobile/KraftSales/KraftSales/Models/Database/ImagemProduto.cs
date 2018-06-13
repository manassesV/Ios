using Realms;

namespace KraftSales.Models.Database
{
    public class ImagemProduto : RealmObject
    {
        [PrimaryKey]
        public string CodigoProduto { get; set; }
        public string Url { get; set; }
    }
}
