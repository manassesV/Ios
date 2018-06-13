using KraftSales.Extensions;
using KraftSales.Helpers;
using KraftSales.Models.Database;
using KraftSales.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KraftSales.Services.Clientes
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteSoapService clienteSoapService;

        public ClienteService() : base("")
        {
            clienteSoapService = DependencyService.Get<IClienteSoapService>();
        }

        public async Task<ObservableCollection<Cliente>> GetAllOfflineAsync()
        {
            var realm = App.RealmContext.GetContext();
            var clientes = realm.All<Cliente>().ToList();

            if (clientes != null)
            {
                return await Task.FromResult(clientes.ToObservableCollection());
            }

            return new ObservableCollection<Cliente>();
        }

        public async Task<ObservableCollection<Cliente>> GetAllOnlineAsync()
        {
            try
            {
                string dataUltimaAlteracao = Settings.DataUltimoDownloadCliente;

                //var list = await GetFromWebApi<List<Cliente>>($"ListarClientes?DataUltAlteracao={dataUltimaAlteracao}");
                var list = await clienteSoapService.GetAllOnlineAsync(dataUltimaAlteracao);
                if (list != null)
                {
                    return list.ToObservableCollection();
                }

                Debug.WriteLine("Erro ao buscar Clientes");
                return new ObservableCollection<Cliente>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new ObservableCollection<Cliente>();
            }
        }

        public async Task<Cliente> GetClienteAsync(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return null;
            }

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            var realm = App.RealmContext.GetContext();
            var cliente = realm.Find<Cliente>(cnpj);
            return await Task.FromResult(cliente);
        }

        public async Task<Models.Clientes.ClienteDkp> GetClienteDkpAsync(string cnpj)
        {
            var result = await PostToWebApi<Models.Clientes.ClienteDkp>("ge1G82s9lv/android/clients/getbyclicgc", new Models.Clientes.ChamadaConsultaClienteDkp(cnpj));
            return result;
        }

        public async Task<int> GetCountClientesOffline()
        {
            var realm = App.RealmContext.GetContext();
            return await Task.FromResult(realm.All<Cliente>().Count());
        }

        public async Task<DateTime?> GetLastBuyDate(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return null;
            }

            cnpj = Regex.Replace(cnpj, @"[^0-9]", "");

            var retorno = await clienteSoapService.GetLastBuyDate(cnpj);
            if (retorno != null)
            {
                if (retorno.UltimaCompra.retorno == "00:00:00")
                {
                    return null;
                }
                else
                {
                    var sucesso = DateTime.TryParse(retorno.UltimaCompra.retorno, out DateTime date);
                    if (sucesso)
                    {
                        return date;
                    }
                }
            }

            return null;
        }

        public async Task<Models.Clientes.RetornoChamadaCadastroCliente> RegisterNewClient(Models.Clientes.Cliente cliente)
        {
            if (cliente != null)
            {
                var clienteNovo = new Models.Clientes.ClienteKraft(cliente);

                var json = JsonConvert.SerializeObject(clienteNovo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var message = await client.PostAsync($"{Settings.UrlPortalBase}/ge1G82s9lv/android/client", content);
                var result = string.Empty;
                if (message.IsSuccessStatusCode)
                {
                    using (var responseStream = await message.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        result = await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false);
                    }

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var retorno1 = JsonConvert.DeserializeObject<Models.Clientes.RetornoChamadaCadastroCliente>(result);

                        if (retorno1 != null && retorno1.cliente != null && !string.IsNullOrWhiteSpace(retorno1.cliente.retorno))
                        {
                            return retorno1;
                        }
                        else
                        {
                            var retorno2 = JsonConvert.DeserializeObject<Models.Clientes.RetornoChamadaCadastroClienteJaExistente>(result);
                            if (retorno2 != null && !string.IsNullOrWhiteSpace(retorno2.CliCgc))
                            {
                                return new Models.Clientes.RetornoChamadaCadastroCliente("cadatrado com sucesso");
                            }
                        }
                    }
                }
            }

            return await Task.FromResult(new Models.Clientes.RetornoChamadaCadastroCliente("Erro ao cadastrar cliente"));
        }

        public async Task<bool> UpsertClientesAsync(List<Cliente> clientes)
        {
            try
            {
                if (clientes != null && clientes.Any())
                {
                    var context = App.RealmContext.GetContext();
                    using (var trans = context.BeginWrite())
                    {
                        foreach (var item in clientes.ToList())
                        {
                            context.Add(item, true);
                        }

                        trans.Commit();
                        Settings.DataUltimoDownloadCliente = DateTime.Now.ToString("dd/MM/yyyy");

                        return await Task.FromResult(true);
                    };
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
