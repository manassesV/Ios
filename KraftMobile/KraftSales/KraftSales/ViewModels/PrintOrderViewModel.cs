using KraftSales.Models.Impressoras;
using KraftSales.Models.Pedidos;
using KraftSales.Services.Impressoras;
using KraftSales.Services.Pedidos;
using KraftSales.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class PrintOrderViewModel : ViewModelBase
    {
        public ICommand PrintOrderCommand => new Command(async () => await PrintOrderAsync());
        public ICommand CancelPrintOrderCommand => new Command(async () => await CancelPrintOrderAsync());
        private IImpressoraService _impressoraService;
        private IPedidoService _pedidoService;
        private Pedido _pedido;

        private ObservableCollection<Impressora> _impressoras;
        public ObservableCollection<Impressora> Impressoras
        {
            get => _impressoras;
            set
            {
                _impressoras = value;
                RaisePropertyChanged(() => Impressoras);
            }
        }

        public PrintOrderViewModel(IImpressoraService impressoraService, IPedidoService pedidoService)
        {
            _impressoraService = impressoraService;
            _pedidoService = pedidoService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                if (navigationData is Pedido pedido)
                {
                    _pedido = pedido;
                }

                Impressoras = await _impressoraService.GetPrintersAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async Task PrintOrderAsync()
        {
            IsBusy = true;

            await _pedidoService.SendOrder(_pedido);

            DialogService.Toast("Pedido realizado com sucesso", 5);
            await NavigationService.GoToRootAsync();

            IsBusy = true;
        }

        private async Task CancelPrintOrderAsync()
        {
            IsBusy = true;

            await NavigationService.GoToRootAsync();

            IsBusy = true;
        }
    }
}
