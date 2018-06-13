using KraftSales.Extensions;
using KraftSales.Models.Pedidos;
using KraftSales.Services.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KraftSales.Services.Representantes
{
    public class RepresentanteService : BaseService, IRepresentanteService
    {
        private readonly IRepresentanteSoapService representanteSoapService;

        public RepresentanteService() : base("")
        {
            representanteSoapService = DependencyService.Get<IRepresentanteSoapService>();
        }

        public async Task<ObservableCollection<Representante>> GetAllOfflineAsync()
        {
            var realm = App.RealmContext.GetContext();
            var representantes = realm.All<Representante>().ToList();

            if (representantes != null)
            {
                return await Task.FromResult(representantes.ToObservableCollection());
            }

            return new ObservableCollection<Representante>();
        }

        public async Task<ObservableCollection<Representante>> GetAllOnlineAsync()
        {
            try
            {
                //var list = await GetFromWebApi<List<Representante>>("ListarVendedores?");
                var list = await representanteSoapService.GetAllOnlineAsync();
                if (list != null)
                {
                    return list.ToObservableCollection();
                }

                Debug.WriteLine("Erro ao buscar representantes");
                return new ObservableCollection<Representante>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ObservableCollection<Representante>();
            }
        }

        public async Task<Representante> GetAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            var realm = App.RealmContext.GetContext();
            var representante = realm.Find<Representante>(codigo);
            return await Task.FromResult(representante);
        }

        public async Task<bool> UpsertItemsAsync(List<Representante> representantes)
        {
            try
            {
                if (representantes != null && representantes.Any())
                {
                    var context = App.RealmContext.GetContext();
                    using (var trans = context.BeginWrite())
                    {
                        foreach (var item in representantes.ToList())
                        {
                            context.Add(item, true);
                        }

                        trans.Commit();
                        //Settings.DataUltimoDownloadCliente = DateTime.Now.ToString("dd/MM/yyyy");

                    };

                    var savedItems = context.All<Representante>().ToList();
                    foreach (var item in savedItems.ToList())
                    {
                        if (!representantes.Any(c => c.Codigo == item.Codigo))
                        {
                            context.Write(() =>
                            {
                                context.Remove(item);
                            });
                        }
                    }

                    return await Task.FromResult(true);

                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
