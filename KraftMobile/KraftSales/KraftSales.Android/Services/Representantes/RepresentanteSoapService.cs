using KraftSales.Models.Pedidos;
using KraftSales.Services.Representantes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.Representantes.RepresentanteSoapService))]
namespace KraftSales.Droid.Services.Representantes
{
    public class RepresentanteSoapService : IRepresentanteSoapService
    {
        wsconsert.wsConsert ws;

        public RepresentanteSoapService()
        {
            ws = new wsconsert.wsConsert();
        }

        public async Task<List<Representante>> GetAllOnlineAsync()
        {
            var resultado = ws.ListarVendedores();
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var lista = JsonConvert.DeserializeObject<List<Representante>>(resultado);
                return await Task.FromResult(lista);
            }

            return new List<Representante>();
        }
    }
}