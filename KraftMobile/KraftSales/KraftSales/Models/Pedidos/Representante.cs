using Realms;

namespace KraftSales.Models.Pedidos
{
    public class Representante : RealmObject
    {
        [PrimaryKey]
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string PercentualComissao { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}



