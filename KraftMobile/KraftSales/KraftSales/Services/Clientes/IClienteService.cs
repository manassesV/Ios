using KraftSales.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Clientes
{
    public interface IClienteService
    {
        Task<ObservableCollection<Cliente>> GetAllOnlineAsync();
        Task<ObservableCollection<Cliente>> GetAllOfflineAsync();
        Task<Cliente> GetClienteAsync(string cnpj);
        Task<bool> UpsertClientesAsync(List<Cliente> clientes);
        Task<int> GetCountClientesOffline();
        Task<Models.Clientes.RetornoChamadaCadastroCliente> RegisterNewClient(Models.Clientes.Cliente cliente);
        Task<DateTime?> GetLastBuyDate(string cnpj);
        Task<Models.Clientes.ClienteDkp> GetClienteDkpAsync(string cnpj);
    }
}
