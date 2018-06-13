using KraftSales.Models.Produtos;
using KraftSales.Services.Grupos;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.Grupos.GrupoService))]
namespace KraftSales.Droid.Services.Grupos
{
    public class GrupoService : IGrupoService
    {
        //private readonly HttpClient client;

        //public GrupoService()
        //{
        //    client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //}

        public async Task<ObservableCollection<Grupo>> GetAllGroupsAsync()
        {
            try
            {
                //string requestUri = $"{App.UrlBase}ListarGrupos?";
                



                //Debug.WriteLine("Erro ao buscar grupos");
                return new ObservableCollection<Grupo>();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                return new ObservableCollection<Grupo>();
            }
        }

        public async Task<Grupo> GetGrupoByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}