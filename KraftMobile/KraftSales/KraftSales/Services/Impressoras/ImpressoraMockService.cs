using KraftSales.Models.Impressoras;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.Services.Impressoras
{
    public class ImpressoraMockService : IImpressoraService
    {
        private ObservableCollection<Impressora> MockPrinters = new ObservableCollection<Impressora>
        {
            new Impressora{ ImpressoraId = Guid.NewGuid().ToString(), Ip = "192.168.0.234", Nome = "Impressora 01"},
            new Impressora{ ImpressoraId = Guid.NewGuid().ToString(), Ip = "192.168.0.235", Nome = "Impressora 02"},
            new Impressora{ ImpressoraId = Guid.NewGuid().ToString(), Ip = "192.168.0.236", Nome = "Impressora 03"},
        };

        public async Task<ObservableCollection<Impressora>> GetPrintersAsync()
        {
            await Task.Delay(500);

            return MockPrinters;
        }
    }
}
