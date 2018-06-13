using System;

namespace KraftSales.Models.Produtos
{
    public class ItemPack
    {
        public ItemPack()
        {
            ItemPackId = Guid.NewGuid().ToString();
        }

        public string ItemPackId { get; set; }
        public int QuantidadeTamanhoP { get; set; }
        public int QuantidadeTamanhoM { get; set; }
        public int QuantidadeTamanhoG { get; set; }
        public string Cor { get; set; }
    }
}
