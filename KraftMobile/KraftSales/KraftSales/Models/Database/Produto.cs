using Realms;

namespace KraftSales.Models.Database
{
    public class Produto : RealmObject
    {
        [PrimaryKey]
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Grupo { get; set; }
        public string Detalhe { get; set; }
        public double Preco { get; set; }
        public string DataUltAlteracao { get; set; }
    }
}
