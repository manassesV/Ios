using KraftSales.Extensions;
using KraftSales.Models.Produtos;
using KraftSales.Services.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KraftSales.Services.Grupos
{
    public class GrupoService : BaseService, IGrupoService
    {
        public GrupoService() : base("")
        {
        }

        public async Task<ObservableCollection<Grupo>> GetAllGroupsAsync()
        {
            try
            {
                //string requestUri = $"{Settings.UrlBase}ListarGrupos?";

                //var response = await client.GetAsync(requestUri).ConfigureAwait(false);

                //if (response.IsSuccessStatusCode)
                //{
                //    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                //    {
                //        var data = await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false);
                //        data = data.Substring(76, data.Length - 85);
                //        return JsonConvert.DeserializeObject<List<Grupo>>(data).ToObservableCollection();
                //    }
                //}

                var list = await GetFromWebApi<List<Grupo>>("ListarGrupos?");
                if (list != null)
                {
                    return list.ToObservableCollection();
                }

                Debug.WriteLine("Erro ao buscar grupos");
                return new ObservableCollection<Grupo>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ObservableCollection<Grupo>();
            }
        }

        public async Task<Grupo> GetGrupoByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
