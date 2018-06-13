using KraftSales.Models.Database;
using KraftSales.Services.Produtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.Produtos.ProdutoSoapService))]
namespace KraftSales.Droid.Services.Produtos
{
    public class ProdutoSoapService : IProdutoSoapService
    {
        wsconsert.wsConsert ws;

        public ProdutoSoapService()
        {
            ws = new wsconsert.wsConsert();
        }

        public async Task<List<Produto>> GetAllOnlineAsync(string dataUltimaAlteracao)
        {
            var resultado = ws.ListarProdutos(dataUltimaAlteracao);
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var lista = JsonConvert.DeserializeObject<List<Produto>>(resultado);
                return await Task.FromResult(lista);
            }

            return new List<Produto>();
        }

        public async Task<int> GetBalanceAvailable(string codigoProduto)
        {
            var resultado = ws.ConsultarSaldo(codigoProduto);
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var balance = JsonConvert.DeserializeObject<Models.Produtos.RetornoSaldo>(resultado);
                if (balance != null)
                {
                    int.TryParse(balance.Saldo.disponivel, out int retorno);
                    return retorno;
                }
            }

            return await Task.FromResult(0);
        }
    }
}