using KraftSales.Helpers;
using KraftSales.Models.Usuarios;
using KraftSales.Services.Clientes;
using KraftSales.Services.Produtos;
using KraftSales.Services.Representantes;
using KraftSales.Services.Usuarios;
using KraftSales.ViewModels.Base;
using KraftSales.Views;
using Plugin.Connectivity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class MainViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());
        public ICommand AddClientCommand => new Command(async () => await AddClientAsync());
        public ICommand OrderCommand => new Command(async () => await OrderAsync());
        public ICommand ApprovalCommand => new Command(async () => await ApprovalAsync());
        public ICommand SyncDataCommand => new Command(async () => await SyncDataAsync());

        private IClienteService _clienteService;
        private IProdutoService _produtoService;
        private IRepresentanteService _representanteService;
        private IUsuarioService _usuarioService;

        private string _currentUser;
        public string CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        public MainViewModel(IClienteService clienteService, IProdutoService produtoService, IRepresentanteService representanteService, IUsuarioService usuarioService)
        {
            _clienteService = clienteService;
            _produtoService = produtoService;
            _representanteService = representanteService;
            _usuarioService = usuarioService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            CurrentUser = Settings.LoggedUser.ToLower();
            await SyncDataAsync();

            IsBusy = false;
        }

        private async Task SyncDataAsync()
        {
            IsBusy = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                bool clientesSincronizados = false;
                bool produtosSincronizados = false;
                bool representantesSincronizados = false;

                try
                {
                    var countClientes = await _clienteService.GetCountClientesOffline();
                    var countProdutos = await _produtoService.GetProdutosOfflineCount();

                    if (countClientes < 1)
                        Settings.DataUltimoDownloadCliente = string.Empty;

                    if (countProdutos < 1)
                        Settings.DataUltimoDownloadProduto = string.Empty;

                    var clientes = await _clienteService.GetAllOnlineAsync();
                    if (await _clienteService.UpsertClientesAsync(clientes.ToList()))
                        clientesSincronizados = true;

                    var produtos = await _produtoService.GetAllOnlineAsync();
                    if (await _produtoService.UpsertProdutosAsync(produtos.ToList()))
                        produtosSincronizados = true;

                    var representantes = await _representanteService.GetAllOnlineAsync();
                    if (await _representanteService.UpsertItemsAsync(representantes.ToList()))
                        representantesSincronizados = true;

                    string login = Settings.LoggedUser;
                    string password = Settings.LoggedUserPassword;

                    var user = await _usuarioService.AuthUser(login, password);
                    if (user != null && user.GridItems.Any())
                    {
                        await _produtoService.UpsertImagemProdutosAsync(user.GridItems.ToList());
                        await _produtoService.UpsertGradeProdutosAsync(user.GridItems.ToList());
                    }

                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                if (clientesSincronizados && produtosSincronizados && representantesSincronizados)
                {
                    DialogService.Toast("Dados sincronizados com sucesso", 3);
                }
                else
                {
                    DialogService.Toast("Erro ao sincronizar dados", 3);
                }
            }

            IsBusy = false;
        }

        private async Task LogoutAsync()
        {
            IsBusy = true;

            //Logout
            await NavigationService.NavigateToAsync<LoginViewModel>(new LogoutParameter { Logout = true });
            await NavigationService.RemoveBackStackAsync();

            IsBusy = false;
        }

        private async Task AddClientAsync()
        {
            IsBusy = true;

            try
            {
                await NavigationService.NavigateToAsync<AddClientViewModel>();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async Task OrderAsync()
        {
            IsBusy = true;

            try
            {
                if (Settings.RepresentantCode.ToLowerInvariant() == "" || Settings.RepresentantCode.ToLowerInvariant() == "0")
                {
                    await DialogService.ShowAlertAsync("Este usuário não tem permissão de fazer pedidos", "", "OK");
                    return;
                }

                await NavigationService.NavigateToAsync<OrderViewModel>();

                //var orderViewModel = new OrderViewModel(_produtoService, _clienteService);
                //var orderPage = new OrderPage() { BindingContext = orderViewModel };
                //await orderViewModel.InitializeAsync(null);

                //if (Application.Current.MainPage is CustomNavigationPage navigationPage)
                //{
                //    await navigationPage.PushAsync(orderPage);
                //}

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async Task ApprovalAsync()
        {
            IsBusy = true;

            try
            {
                await NavigationService.NavigateToAsync<OrdersToApproveViewModel>();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            throw new System.NotImplementedException();
        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            throw new System.NotImplementedException();
        }
    }
}
