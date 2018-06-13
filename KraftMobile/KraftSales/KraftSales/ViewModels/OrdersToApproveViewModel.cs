using KraftSales.Models.Pedidos;
using KraftSales.Services.Pedidos;
using KraftSales.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class OrdersToApproveViewModel : ViewModelBase
    {
        public ICommand ApproveItemCommand => new Command<Pedido>(ApproveItem);

        private ObservableCollection<Pedido> _pedidos;
        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set
            {
                _pedidos = value;
                RaisePropertyChanged(() => Pedidos);
            }
        }

        private IPedidoService _pedidoService;

        public OrdersToApproveViewModel(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                Pedidos = await _pedidoService.GetOrdersToApproveAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            MessagingCenter.Unsubscribe<ApprovalViewModel, Pedido>(this, MessageKeys.OrderRejected);
            MessagingCenter.Subscribe<ApprovalViewModel, Pedido>(this, MessageKeys.OrderRejected, async (sender, pedido) =>
            {
                await RemovePedidoFromList(pedido);
            });

            IsBusy = false;
        }

        private async Task RemovePedidoFromList(Pedido pedido)
        {
            IsBusy = true;

            if (Pedidos != null && Pedidos.Any())
            {
                var pedidoFound = Pedidos.FirstOrDefault(c => c.PedidoId == pedido.PedidoId);
                if (pedidoFound != null)
                {
                    Pedidos.Remove(pedidoFound);
                }
            }

            await Task.FromResult(0);

            IsBusy = false;

        }

        private void ApproveItem(Pedido pedido)
        {
            IsBusy = true;

            NavigationService.NavigateToAsync<ApprovalViewModel>(pedido);

            IsBusy = false;
        }
    }
}
