using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KraftSales.Models.Produtos
{
    public class Produto
    {
        public Produto()
        {
            ItensPackMinimo = new List<Models.Database.GradeProduto>();
            ItensCaixaMinimo = new List<ItemPack>();
        }

        public string ProdutoId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        [JsonProperty("Grupo")]
        public string GrupoId { get; set; }
        [JsonProperty("Detalhe")]
        public string DetalheId { get; set; }
        public double Preco { get; set; }
        public int QuantidadePacksEmEstoque { get; set; }
        public int QuantidadeCaixasEmEstoque { get; set; }
        public ICollection<Models.Database.GradeProduto> ItensPackMinimo { get; set; }
        public int QuantidadeItensPack
        {
            get
            {
                if (ItensPackMinimo == null || !ItensPackMinimo.Any())
                {
                    return 0;
                }

                return ItensPackMinimo.Sum(c=> c.Quantidade);
            }
        }
        public ICollection<ItemPack> ItensCaixaMinimo { get; set; }
        public int QuantidadeItensCaixa
        {
            get
            {
                if (ItensCaixaMinimo == null || !ItensCaixaMinimo.Any())
                {
                    return 0;
                }

                return ItensCaixaMinimo.Sum(c => c.QuantidadeTamanhoP) + ItensCaixaMinimo.Sum(c => c.QuantidadeTamanhoM) + ItensCaixaMinimo.Sum(c => c.QuantidadeTamanhoG);
            }
        }
        public string ImagemUrl { get; set; }

        public string TipoItemEscolhido { get; set; }
        public int QuantidadeEscolhida { get; set; }

        public Database.Produto ProdutoInfo { get; set; }

        public bool PedidoDeNovoCliente { get; set; }
    }
}
