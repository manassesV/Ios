using KraftSales.Extensions;
using KraftSales.Helpers;
using KraftSales.Models.Clientes;
using KraftSales.Models.Pedidos;
using KraftSales.Services.Clientes;
using KraftSales.Services.Produtos;
using KraftSales.Validations;
using KraftSales.ViewModels.Base;
using KraftSales.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        public ICommand AddOrderItemCommand => new Command(async () => await AddOrderItemAsync());
        public ICommand CancelOrderCommand => new Command(async () => await CancelOrderAsync());
        public ICommand PaymentCommand => new Command(async () => await PaymentAsync());
        public ICommand ValidateCnpjCommand => new Command(() => ValidateCnpj());
        public ICommand ValidateRazaoSocialCommand => new Command(() => ValidateRazaoSocial());
        public ICommand ValidateNomeFantasiaCommand => new Command(() => ValidateNomeFantasia());
        public ICommand RemoveItemCommand => new Command<ItemPedido>(RemoveItem);
        public ICommand EditItemCommand => new Command<ItemPedido>(EditItemPedidoAsync);

        public static ObservableCollection<string> _productsCodes = new ObservableCollection<string>();

        public static readonly BindableProperty ProductCodeProperty = BindableProperty.Create(nameof(ProductCode),
            typeof(string),
            typeof(MainPage),
            default(string),
            propertyChanged: ProductCodePropertyChanged);

        public static readonly BindableProperty ProductsSuggestionsProperty = BindableProperty.Create(nameof(ProductsSuggestions),
                typeof(ObservableCollection<string>),
                typeof(MainPage),
                new ObservableCollection<string>());
        
        private object _selectedProductCode;
        public object SelectedProductCode
        {
            get => _selectedProductCode;
            set
            {
                _selectedProductCode = value;
                RaisePropertyChanged(() => SelectedProductCode);
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    ProductCode = value.ToString();
                    //CodigoProduto = value.ToString();
                    AddOrderItemAsync();
                }
            }
        }

        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } =
            (text, values) => values
                .Where(x => x.ToLower().Contains(text.ToLower()))
                .OrderBy(x => x)
                .ToList();

        public ObservableCollection<string> ProductsSuggestions
        {
            get { return (ObservableCollection<string>)GetValue(ProductsSuggestionsProperty); }
            set { SetValue(ProductsSuggestionsProperty, value); }
        }

        public string ProductCode
        {
            get { return (string)GetValue(ProductCodeProperty); }
            set { SetValue(ProductCodeProperty, value); }
        }

        public static void ProductCodePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            try
            {
                var model = (OrderViewModel)bindable;
                var text = newvalue.ToString().ToUpper();

                if (string.IsNullOrEmpty(text) || text.Length < 3) return;

                model.ProductsSuggestions.Clear();

                var results = _productsCodes.Where(c => c.Contains(text)).ToArray();

                for (int i = 0; i < results.Count(); i++)
                {
                    model.ProductsSuggestions.Add(results[i]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private Pedido _pedido;
        private int _auxCount = 0;
        private bool EhClienteDkp = false;

        private IProdutoService _produtoService;
        private IClienteService _clienteService;

        private bool podeEditarDadosCliente;
        public bool PodeEditarDadosCliente
        {
            get => podeEditarDadosCliente;
            set
            {
                podeEditarDadosCliente = value;
                RaisePropertyChanged(() => PodeEditarDadosCliente);
            }
        }

        private List<string> tiposCliente = new List<string> { "Pessoa Jurídica", "Pessoa Física" };
        public List<string> TiposCliente => tiposCliente;

        private int _tipoClienteSelectedIndex;
        public int TipoClienteSelectedIndex
        {
            get => _tipoClienteSelectedIndex;
            set
            {
                _tipoClienteSelectedIndex = value;
                RaisePropertyChanged(() => TipoClienteSelectedIndex);
                PessoaJuridica = _tipoClienteSelectedIndex < 1;
                if (_auxCount > 2)
                {
                    _cnpj.Value = string.Empty;
                }
                else
                {
                    _auxCount++;
                }
            }
        }

        private bool _pessoaJuridica = true;
        public bool PessoaJuridica
        {
            get => _pessoaJuridica;
            set
            {
                _pessoaJuridica = value;
                NomeCodigoCliente = value ? "CNPJ" : "CPF";
                RaisePropertyChanged(() => PessoaJuridica);
            }
        }

        private string _nomeCodigoCliente;
        public string NomeCodigoCliente
        {
            get => _nomeCodigoCliente;
            set
            {
                _nomeCodigoCliente = value;
                RaisePropertyChanged(() => NomeCodigoCliente);
            }
        }

        private ValidatableObject<string> _cnpj;
        public ValidatableObject<string> Cnpj
        {
            get => _cnpj;
            set
            {
                _cnpj = value;
                RaisePropertyChanged(() => Cnpj);
            }
        }

        private ValidatableObject<string> _razaoSocial;
        public ValidatableObject<string> RazaoSocial
        {
            get => _razaoSocial;
            set
            {
                _razaoSocial = value;
                RaisePropertyChanged(() => RazaoSocial);
            }
        }

        private ValidatableObject<string> _nomeFantasia;
        public ValidatableObject<string> NomeFantasia
        {
            get => _nomeFantasia;
            set
            {
                _nomeFantasia = value;
                RaisePropertyChanged(() => NomeFantasia);
            }
        }

        //private string _codigoProduto;
        //public string CodigoProduto
        //{
        //    get => _codigoProduto;
        //    set
        //    {
        //        _codigoProduto = value;
        //        RaisePropertyChanged(() => CodigoProduto);
        //    }
        //}

        private int _totalItens = 0;
        public int TotalItens
        {
            get => _totalItens;
            set
            {
                _totalItens = value;
                RaisePropertyChanged(() => TotalItens);
            }
        }

        private double _valorTotal = 0.0;
        public double ValorTotal
        {
            get => _valorTotal;
            set
            {
                _valorTotal = value;
                RaisePropertyChanged(() => ValorTotal);
            }
        }

        private ObservableCollection<ItemPedido> _itensPedidos;
        public ObservableCollection<ItemPedido> ItensPedidos
        {
            get => _itensPedidos;
            set
            {
                _itensPedidos = value;
                RaisePropertyChanged(() => ItensPedidos);
            }
        }

        public bool PedidoDeClienteNovo { get; set; } = false;

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        private bool _clienteSelecionado;
        public bool ClienteSelecionado
        {
            get => _clienteSelecionado;
            set
            {
                _clienteSelecionado = value;
                RaisePropertyChanged(() => ClienteSelecionado);
            }
        }

        public OrderViewModel(IProdutoService produtoService, IClienteService clienteService)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;

            _cnpj = new ValidatableObject<string>();
            _razaoSocial = new ValidatableObject<string>();
            _nomeFantasia = new ValidatableObject<string>();
            //_dataEntrega = new ValidatableObject<string>();

            _pedido = new Pedido();
            ItensPedidos = new ObservableCollection<ItemPedido>();

            AddValidations();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (navigationData is Cliente cliente)
                {
                    RazaoSocial.Value = cliente.RazaoSocial;
                    NomeFantasia.Value = cliente.NomeFantasia;
                    Cnpj.Value = cliente.Cnpj;

                    TipoClienteSelectedIndex = cliente.TipoCliente == Enums.TipoCliente.PessoaJuridica ? 0 : 1;

                    PodeEditarDadosCliente = false;
                    PedidoDeClienteNovo = true;
                    ClienteSelecionado = true;

                    string ddd = string.Empty;
                    string telefone = string.Empty;
                    string telefoneLimpo = Regex.Replace(cliente.TelefoneContato ?? string.Empty, @"[^0-9]", "");
                    if (telefoneLimpo.Length > 1)
                    {
                        ddd = telefoneLimpo.Substring(0, 2);
                    }

                    if (telefoneLimpo.Length > 2)
                    {
                        telefone = telefoneLimpo.Substring(2);
                    }

                    _pedido.Cliente = new Models.Database.Cliente
                    {
                        CliCgc = Regex.Replace(cliente.Cnpj, @"[^0-9]", ""),
                        CliNom = cliente.RazaoSocial.ToUpper(),
                        CliNomRdz = cliente.NomeFantasia.ToUpper(),
                        CliCntCom = cliente.NomeContato.ToUpper(),
                        CliEndEle = cliente.EmailContato.ToUpper(),
                        CliDddCom = ddd,
                        CliTelCom = telefone,
                    };
                }
                else
                {
                    PodeEditarDadosCliente = true;
                    ClienteSelecionado = false;
                }

                _productsCodes = await _produtoService.GetAllCodesOfflineAsync();

                MessagingCenter.Unsubscribe<AddOrderItemViewModel, ItemPedido>(this, MessageKeys.AddProduct);
                MessagingCenter.Subscribe<AddOrderItemViewModel, ItemPedido>(this, MessageKeys.AddProduct, async (sender, arg) =>
                {
                    //BadgeCount++;

                    await AddItemToListAsync(arg);
                });

                MessagingCenter.Unsubscribe<NfcHelper, string>(this, MessageKeys.NfcTagRead);
                MessagingCenter.Subscribe<NfcHelper, string>(this, MessageKeys.NfcTagRead, async (sender, tagLida) =>
                {
                    await NfcTagRead(tagLida);
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private async Task NfcTagRead(string tagLida)
        {
            if (NavigationService.CurrentPageViewModel is OrderViewModel && ClienteSelecionado)
            {
                tagLida = tagLida.ToUpper().Trim();
                tagLida = tagLida.Replace("", "");

                if (tagLida.StartsWith("EN"))
                {
                    tagLida = tagLida.Remove(0, 2);
                }

                //CodigoProduto = tagLida;
                ProductCode = tagLida;
                await AddOrderItemAsync();
            }
        }

        private async Task AddItemToListAsync(ItemPedido item)
        {
            if (ItensPedidos.Any(c => c.CodigoProduto == item.CodigoProduto))
            {
                ItensPedidos.Remove(ItensPedidos.First(c => c.CodigoProduto == item.CodigoProduto));
            }

            ItensPedidos.Add(item);

            ItensPedidos = ItensPedidos.OrderBy(c => c.CodigoProduto).ToObservableCollection();

            await ReCalculateTotalAsync();
        }

        private async Task ReCalculateTotalAsync()
        {
            int totalItens = 0;
            double totalPrice = 0.0;

            if (ItensPedidos != null && ItensPedidos.Any())
            {
                totalPrice = ItensPedidos.Sum(c => c.Valor);
                totalItens = ItensPedidos.Sum(c => c.Quantidade);
            }

            TotalItens = totalItens;
            ValorTotal = totalPrice;

            await Task.FromResult(0);
        }

        private async Task AddOrderItemAsync()
        {
            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(ProductCode))
                {
                    await DialogService.ShowAlertAsync("Insira um código de produto ou leia uma Tag NFC", "", "OK");
                    IsBusy = false;
                    return;
                }

                //var produto = await _produtoService.GetProdutoAsync(CodigoProduto.Trim());
                var produto = await _produtoService.GetProdutoByCodigoMockAsync(ProductCode.Trim());

                if (produto == null)
                {
                    await DialogService.ShowAlertAsync("Produto não encontrado. O Código está correto?", "", "OK");
                    IsBusy = false;
                    return;
                }

                produto.PedidoDeNovoCliente = PedidoDeClienteNovo;
                ProductCode = string.Empty;
                SelectedProductCode = string.Empty;
                await NavigationService.NavigateToAsync<AddOrderItemViewModel>(produto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async void EditItemPedidoAsync(ItemPedido item)
        {
            IsBusy = true;

            try
            {
                var produto = await _produtoService.GetProdutoByCodigoMockAsync(item.CodigoProduto);

                //if (produto == null)
                //{
                //    await DialogService.ShowAlertAsync("Produto não encontrado. O Código está correto?", "", "OK");
                //    IsBusy = false;
                //    return;
                //}

                if (produto != null)
                {
                    produto.QuantidadeEscolhida = item.QuantidadeEscolhida;
                    produto.TipoItemEscolhido = item.TipoItem;

                    ProductCode = string.Empty;
                    SelectedProductCode = string.Empty;
                    await NavigationService.NavigateToAsync<AddOrderItemViewModel>(produto);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        private async void RemoveItem(ItemPedido itemPedido)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                //if (!CrossConnectivity.Current.IsConnected)
                //{
                //    await DialogService.ShowAlertAsync("Dispositivo sem conexão", "", "OK");
                //    return;
                //}

                if (itemPedido == null)
                {
                    return;
                }

                IsBusy = true;

                ItensPedidos.Remove(itemPedido);
                await ReCalculateTotalAsync();


                DialogService.Toast("Item removido com sucesso", 3);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;

            await Task.FromResult(0);
        }

        private async Task PaymentAsync()
        {
            if (TotalItens < 1)
            {
                await DialogService.ShowAlertAsync("Adicione ao menos um produto para continuar", "", "OK");
                return;
            }

            IsBusy = true;

            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    _pedido.PedidoDeClienteNovo = PedidoDeClienteNovo;
                    _pedido.StatusPedido = StatusPedido.Aberto;
                    _pedido.ValorTotal = ValorTotal;
                    _pedido.Itens = ItensPedidos.ToList();
                    _pedido.TotalItens = TotalItens;
                    _pedido.CnpjCliente = Cnpj.Value;
                    //CanReadTags = false;

                    if (!_pedido.PedidoDeClienteNovo)
                    {
                        var lastBuyDate = await _clienteService.GetLastBuyDate(_cnpj.Value);
                        if (lastBuyDate == null || lastBuyDate.Value < DateTime.Now.AddYears(-1))
                        {
                            _pedido.PedidoDeClienteNovo = true;
                            _pedido.ClienteNaoCompraHaMaisDeDozeMeses = true;
                            await DialogService.ShowAlertAsync("Cliente não compra há mais de doze meses", "Atenção", "OK");
                        }
                    }

                    await NavigationService.NavigateToAsync<PaymentViewModel>(_pedido);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SaveAsync] Error => {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            //if (isValid)
            //{
            //    _pedido.PedidoDeClienteNovo = PedidoDeClienteNovo;
            //    _pedido.StatusPedido = StatusPedido.Aberto;
            //    _pedido.ValorTotal = ValorTotal;
            //    _pedido.Itens = ItensPedidos.ToList();
            //    _pedido.TotalItens = TotalItens;
            //    CanReadTags = false;
            //    await NavigationService.NavigateToAsync<PaymentViewModel>(_pedido);
            //}

            IsBusy = false;
        }

        private async Task CancelOrderAsync()
        {
            var result = await DialogService.ConfirmAsync("Você deseja mesmo cancelar o pedido?", "ATENÇÃO!", "Sim", "Não");
            if (result)
                await NavigationService.GoBackAsync();
        }

        private bool Validate()
        {
            bool isValidCnpj = ValidateCnpj();

            if (isValidCnpj)
            {
                isValidCnpj = ValidateCodigoCliente();
            }

            //bool isValidRazaoSocial = ValidateRazaoSocial();
            //bool isValidNomeFantasia = ValidateNomeFantasia();
            //bool isValidDataEntrega = ValidateDataEntrega();

            return isValidCnpj/* && isValidRazaoSocial && isValidNomeFantasia*/;/* && isValidDataEntrega;*/
        }

        private bool ValidateCodigoCliente()
        {
            if (podeEditarDadosCliente)
            {
                var cliente = _clienteService.GetClienteAsync(_cnpj.Value).Result;

                if (cliente != null)
                {
                    return true;
                }
                else if (!EhClienteDkp)
                {
                    Cnpj.IsValid = false;
                    Cnpj.Errors = new List<string> { "Cliente não encontrado" };
                    return false;
                }
            }
            return true;
        }

        private bool ValidateCnpj()
        {
            bool clienteEncontrado = true;
            string msgErro = string.Empty;
            EhClienteDkp = false;

            if (!string.IsNullOrWhiteSpace(_cnpj.Value))
            {
                string text = Regex.Replace(_cnpj.Value, @"[^0-9]", "");

                if (PessoaJuridica)
                {
                    text = text.PadRight(14);

                    if (text.Length > 14)
                    {
                        text = text.Remove(14);
                    }

                    text = text.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-").TrimEnd(new char[] { ' ', '.', '/', '-' });

                    if (PodeEditarDadosCliente && text.Length == 18)
                    {
                        var cliente = _clienteService.GetClienteAsync(text).Result;
                        if (cliente != null)
                        {
                            RazaoSocial.Value = cliente.CliNom;
                            NomeFantasia.Value = cliente.CliNomRdz;
                            _pedido.Cliente = cliente;
                            ClienteSelecionado = true;
                            PedidoDeClienteNovo = false;
                        }
                        else
                        {
                            var task = Task.Run<ClienteDkp>(async () => await _clienteService.GetClienteDkpAsync(text));
                            var clienteDkp = task.Result;
                            if (clienteDkp != null && !string.IsNullOrWhiteSpace(clienteDkp.CliId))
                            {
                                RazaoSocial.Value = clienteDkp.CliNom;
                                NomeFantasia.Value = clienteDkp.CliNomRdz;
                                _pedido.Cliente = new Models.Database.Cliente
                                {
                                    CliCgc = clienteDkp.CliCgc,
                                    CliNom = clienteDkp.CliNom,
                                    CliNomRdz = clienteDkp.CliNomRdz,
                                    CliEndFatSep = clienteDkp.CliEndFatSep,
                                    NroEndFat = clienteDkp.NroEndFat,
                                    ComplEndFat = clienteDkp.ComplEndFat,
                                    CliBaiFat = clienteDkp.CliBaiFat,
                                    CliCidFat = clienteDkp.CliCidFat,
                                    CliEstFat = clienteDkp.CliEstFat,
                                    CliCepFat = clienteDkp.CliCepFat,
                                    CliDddCom = clienteDkp.CliDddCom,
                                    CliTelCom = clienteDkp.CliTelCom,
                                    CliInsFat = clienteDkp.CliInsFat,
                                    CliCntCom = clienteDkp.CliCntCom,
                                    CliEndEle = clienteDkp.CliEndEle,
                                    CliInsSfr = clienteDkp.CliInsSfr,
                                    CliMunSfr = clienteDkp.CliMunSfr,
                                    CliDtUltAlt = clienteDkp.CliDtUltAlt,
                                };
                                ClienteSelecionado = true;
                                PedidoDeClienteNovo = clienteDkp.CliStatus.ToUpper() == StatusClienteDkp.NOVO;
                                EhClienteDkp = true;
                            }
                            else
                            {
                                clienteEncontrado = false;
                                msgErro = "Não foi encontrado nenhum cliente com este CNPJ";
                                RazaoSocial.Value = string.Empty;
                                NomeFantasia.Value = string.Empty;
                            }
                        }
                    }
                    else if (PodeEditarDadosCliente && text.Length < 18)
                    {
                        ClienteSelecionado = false;
                        RazaoSocial.Value = string.Empty;
                        NomeFantasia.Value = string.Empty;
                    }
                }
                else
                {
                    text = text.PadRight(11);

                    if (text.Length > 11)
                    {
                        text = text.Remove(11);
                    }

                    text = text.Insert(3, ".").Insert(7, ".").Insert(11, "-").TrimEnd(new char[] { ' ', '.', '-' });

                    if (PodeEditarDadosCliente && text.Length == 14)
                    {
                        var cliente = _clienteService.GetClienteAsync(text).Result;
                        if (cliente != null)
                        {
                            RazaoSocial.Value = cliente.CliNom;
                            NomeFantasia.Value = cliente.CliNomRdz;
                            _pedido.Cliente = cliente;
                            ClienteSelecionado = true;
                            PedidoDeClienteNovo = false;
                        }
                        else
                        {
                            var task = Task.Run<ClienteDkp>(async () => await _clienteService.GetClienteDkpAsync(text));
                            var clienteDkp = task.Result;
                            if (clienteDkp != null && !string.IsNullOrWhiteSpace(clienteDkp.CliId))
                            {
                                RazaoSocial.Value = clienteDkp.CliNom;
                                NomeFantasia.Value = clienteDkp.CliNomRdz;
                                _pedido.Cliente = new Models.Database.Cliente
                                {
                                    CliCgc = clienteDkp.CliCgc,
                                    CliNom = clienteDkp.CliNom,
                                    CliNomRdz = clienteDkp.CliNomRdz,
                                    CliEndFatSep = clienteDkp.CliEndFatSep,
                                    NroEndFat = clienteDkp.NroEndFat,
                                    ComplEndFat = clienteDkp.ComplEndFat,
                                    CliBaiFat = clienteDkp.CliBaiFat,
                                    CliCidFat = clienteDkp.CliCidFat,
                                    CliEstFat = clienteDkp.CliEstFat,
                                    CliCepFat = clienteDkp.CliCepFat,
                                    CliDddCom = clienteDkp.CliDddCom,
                                    CliTelCom = clienteDkp.CliTelCom,
                                    CliInsFat = clienteDkp.CliInsFat,
                                    CliCntCom = clienteDkp.CliCntCom,
                                    CliEndEle = clienteDkp.CliEndEle,
                                    CliInsSfr = clienteDkp.CliInsSfr,
                                    CliMunSfr = clienteDkp.CliMunSfr,
                                    CliDtUltAlt = clienteDkp.CliDtUltAlt,
                                };
                                ClienteSelecionado = true;
                                PedidoDeClienteNovo = clienteDkp.CliStatus.ToUpper() == StatusClienteDkp.NOVO;
                                EhClienteDkp = true;
                            }
                            else
                            {
                                clienteEncontrado = false;
                                msgErro = "Não foi encontrado nenhum cliente com este CPF";
                                RazaoSocial.Value = string.Empty;
                                NomeFantasia.Value = string.Empty;
                            }
                        }
                    }
                    else if (PodeEditarDadosCliente && text.Length < 14)
                    {
                        ClienteSelecionado = false;
                        RazaoSocial.Value = string.Empty;
                        NomeFantasia.Value = string.Empty;
                    }
                }

                if (_cnpj.Value != text)
                {
                    _cnpj.Value = text;
                }

                if (!clienteEncontrado)
                {
                    Cnpj.IsValid = false;
                    Cnpj.Errors = new List<string> { msgErro };
                    ClienteSelecionado = false;
                    return false;
                }
            }

            return _cnpj.Validate();
        }

        private bool ValidateRazaoSocial()
        {
            return _razaoSocial.Validate();
        }

        private bool ValidateNomeFantasia()
        {
            return _nomeFantasia.Validate();
        }

        //private bool ValidateDataEntrega()
        //{
        //    return _dataEntrega.Validate();
        //}

        private void AddValidations()
        {
            _cnpj.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _razaoSocial.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _nomeFantasia.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            //_dataEntrega.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Escolha a Data de Entrega." });
        }

        //private void OnDisappearing()
        //{
        //    CanReadTags = false;
        //}
    }
}
