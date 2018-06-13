using KraftSales.Models.Database;
using KraftSales.Services.Clientes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.Clientes.ClienteSoapService))]
namespace KraftSales.Droid.Services.Clientes
{
    public class ClienteSoapService : IClienteSoapService
    {
        wsconsert.wsConsert ws;

        public ClienteSoapService()
        {
            ws = new wsconsert.wsConsert();
        }

        public async Task<List<Cliente>> GetAllOnlineAsync(string dataUltimaAlteracao)
        {
            var resultado = ws.ListarClientes(dataUltimaAlteracao);
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var lista = JsonConvert.DeserializeObject<List<Cliente>>(resultado);
                return await Task.FromResult(lista);
            }

            return new List<Cliente>();
        }

        public async Task<Models.Clientes.RetornoChamadaUltimaCompra> GetLastBuyDate(string cnpj)
        {
            var resultado = ws.ConsultarUltimaCompra(cnpj);
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var retorno = JsonConvert.DeserializeObject<Models.Clientes.RetornoChamadaUltimaCompra>(resultado);
                return await Task.FromResult(retorno);
            }

            return null;
        }
    }
}