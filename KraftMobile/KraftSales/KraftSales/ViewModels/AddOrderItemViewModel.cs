using KraftSales.Extensions;
using KraftSales.Models.Pedidos;
using KraftSales.Models.Produtos;
using KraftSales.Services.Produtos;
using KraftSales.Validations;
using KraftSales.ViewModels.Base;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class AddOrderItemViewModel : ViewModelBase
    {
        //private ValidatableObject<string> _quantidade;
        //public ValidatableObject<string> Quantidade
        //{
        //    get { return _quantidade; }
        //    set
        //    {
        //        _quantidade = value;
        //        RaisePropertyChanged(() => Quantidade);
        //        ReCalculateQuantidadeTotalItens();
        //    }
        //}

        private int _quantidade;
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                _quantidade = value;
                RaisePropertyChanged(() => Quantidade);
                ReCalculateQuantidadeTotalItens();
            }
        }

        private int _quantidadeTotalItens;
        public int QuantidadeTotalItens
        {
            get => _quantidadeTotalItens;
            set
            {
                _quantidadeTotalItens = value;
                RaisePropertyChanged(() => QuantidadeTotalItens);
            }
        }

        private string _codigoProduto;
        public string CodigoProduto
        {
            get => _codigoProduto;
            set
            {
                _codigoProduto = value;
                RaisePropertyChanged(() => CodigoProduto);
            }
        }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                RaisePropertyChanged(() => Descricao);
            }
        }

        private string _preco;
        public string Preco
        {
            get => _preco;
            set
            {
                _preco = value;
                RaisePropertyChanged(() => Preco);
            }
        }

        private int _quantidadePackMinimo;
        public int QuantidadePackMinimo
        {
            get => _quantidadePackMinimo;
            set
            {
                _quantidadePackMinimo = value;
                RaisePropertyChanged(() => QuantidadePackMinimo);
            }
        }

        private int _quantidadeCaixaMinimo;
        public int QuantidadeCaixaMinimo
        {
            get => _quantidadeCaixaMinimo;
            set
            {
                _quantidadeCaixaMinimo = value;
                RaisePropertyChanged(() => QuantidadeCaixaMinimo);
            }
        }

        //private ObservableCollection<ItemPack> _itensPackMinimo;
        //public ObservableCollection<ItemPack> ItensPackMinimo
        //{
        //    get => _itensPackMinimo;
        //    set
        //    {
        //        _itensPackMinimo = value;
        //        RaisePropertyChanged(() => ItensPackMinimo);
        //    }
        //}

        private ObservableCollection<Models.Database.GradeProduto> _itensPackMinimo;
        public ObservableCollection<Models.Database.GradeProduto> ItensPackMinimo
        {
            get => _itensPackMinimo;
            set
            {
                _itensPackMinimo = value;
                RaisePropertyChanged(() => ItensPackMinimo);
            }
        }


        private string _productUrl = "mochine250x270black";
        public string ProductUrl
        {
            get => _productUrl;
            set
            {
                _productUrl = value;
                RaisePropertyChanged(() => ProductUrl);
            }
        }

        //private ObservableCollection<ItemPack> _itensCaixaMinimo;
        //public ObservableCollection<ItemPack> ItensCaixaMinimo
        //{
        //    get => _itensCaixaMinimo;
        //    set
        //    {
        //        _itensCaixaMinimo = value;
        //        RaisePropertyChanged(() => ItensCaixaMinimo);
        //    }
        //}

        private List<string> tiposItem = new List<string> { "Unidade", "Pack"/*, "Caixa"*/ };
        public List<string> TiposItem => tiposItem;

        //private int _tipoItemSelectedIndex = 1;
        //public int TipoItemSelectedIndex
        //{
        //    get => _tipoItemSelectedIndex;
        //    set
        //    {
        //        _tipoItemSelectedIndex = value;
        //        RaisePropertyChanged(() => TipoItemSelectedIndex);
        //        TipoItem = TiposItem[_tipoItemSelectedIndex];
        //        ReCalculateQuantidadeTotalItens();
        //    }
        //}

        private string _tipoItem;
        public string TipoItem
        {
            get => _tipoItem;
            set
            {
                _tipoItem = value;
                RaisePropertyChanged(() => TipoItem);
                ReCalculateQuantidadeTotalItens();
            }
        }

        private int _saldoDisponivel;
        public int SaldoDisponivel
        {
            get => _saldoDisponivel;
            set
            {
                _saldoDisponivel = value;
                RaisePropertyChanged(() => SaldoDisponivel);
            }
        }

        private bool _exibeSaldo;
        public bool ExibeSaldo
        {
            get => _exibeSaldo;
            set
            {
                _exibeSaldo = value;
                RaisePropertyChanged(() => ExibeSaldo);
            }
        }

        public ICommand OpenImageCommand => new Command(async () => await OpenImageAsync());
        public ICommand AddItemCommand => new Command(async () => await AddItemAsync());
        public ICommand ValidateQuantidadeCommand => new Command(() => ValidateSaldoDisponivel());

        public Produto Produto;

        private IProdutoService _produtoService;

        public AddOrderItemViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;

            //_quantidade = new ValidatableObject<string>();
            //ItensPackMinimo = new ObservableCollection<ItemPack>();
            ItensPackMinimo = new ObservableCollection<Models.Database.GradeProduto>();
            //ItensCaixaMinimo = new ObservableCollection<ItemPack>();

            AddValidations();
        }

        private async Task AddItemAsync()
        {
            if (Quantidade < 1)
            {
                await DialogService.ShowAlertAsync($"Por favor, escolha a quantidade.", "", "OK");
                return;
            }

            IsBusy = true;

            bool saldoEstaDisponivel = ValidateSaldoDisponivel();

            if (saldoEstaDisponivel)
            {
                await Task.Delay(1000);
                int quantidade = QuantidadeTotalItens;
                MessagingCenter.Send(this, MessageKeys.AddProduct, new ItemPedido
                {
                    CodigoProduto = CodigoProduto,
                    Quantidade = quantidade,
                    Valor = Produto.Preco * quantidade,
                    ProdutoId = Produto.ProdutoId,
                    //TipoItem = TipoItem == "Caixa" ? Models.Pedidos.TipoItem.Caixa : Models.Pedidos.TipoItem.Pack,
                    TipoItem = TipoItem,
                    QuantidadeEscolhida = Quantidade,
                    Descricao = Descricao,
                });
                await NavigationService.GoBackAsync();
            }
            else
            {
                await DialogService.ShowAlertAsync($"Quantidade ultrapassa o saldo disponível de {SaldoDisponivel} peça(s).", "", "OK");
            }

            IsBusy = false;
        }

        private async Task OpenImageAsync()
        {
            IsBusy = true;
            await NavigationService.NavigateToAsync<ProductImageViewModel>(Produto);
            IsBusy = false;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is Produto produto)
            {
                try
                {
                    Produto = produto;
                    CodigoProduto = produto.Codigo;
                    Descricao = produto.Descricao;
                    Preco = produto.Preco.ToString("F");
                    //QuantidadePackMinimo = produto.QuantidadeItensPack;
                    QuantidadeCaixaMinimo = produto.QuantidadeItensCaixa;
                    ProductUrl = await _produtoService.GetImagemProdutoAsync(produto.Codigo);
                    Produto.ImagemUrl = ProductUrl;

                    //ItensCaixaMinimo = produto.ItensCaixaMinimo.ToObservableCollection();
                    //ItensPackMinimo = produto.ItensPackMinimo.ToObservableCollection();
                    ItensPackMinimo = await _produtoService.GetGradeProduto(produto.Codigo);
                    QuantidadePackMinimo = ItensPackMinimo.Any() ? ItensPackMinimo.Sum(c => c.Quantidade) : 1;
                    produto.ItensPackMinimo = _itensPackMinimo.ToList();

                    if (string.IsNullOrWhiteSpace(produto.TipoItemEscolhido))
                    {
                        Quantidade = 0;
                        TipoItem = "Unidade";
                        //TipoItem = "Pack";
                    }
                    else
                    {
                        Quantidade = produto.QuantidadeEscolhida;
                        TipoItem = produto.TipoItemEscolhido;
                    }

                    if (!produto.PedidoDeNovoCliente && CrossConnectivity.Current.IsConnected)
                    {
                        SaldoDisponivel = await _produtoService.GetBalanceAvailable(produto.Codigo);
                        ExibeSaldo = true;
                    }
                    else
                    {
                        SaldoDisponivel = -1;
                        ExibeSaldo = false;
                    }

                    MessagingCenter.Send(this, MessageKeys.OrderItemGraphLoaded);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private bool ValidateSaldoDisponivel()
        {
            if (!ExibeSaldo)
            {
                return true;
            }

            if (SaldoDisponivel >= QuantidadeTotalItens)
            {
                return true;
            }

            return false;
        }

        private void AddValidations()
        {
            //_quantidade.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Insira a quantidade" });
        }

        private void ReCalculateQuantidadeTotalItens()
        {
            int quantidadeTotalItens = 0;
            int quantidadeItensCaixa = 0;
            int quantidadeItensPack = 0;

            if (Produto != null)
            {
                quantidadeItensCaixa = Produto.QuantidadeItensCaixa > 0 ? Produto.QuantidadeItensCaixa : 1;
                quantidadeItensPack = Produto.QuantidadeItensPack > 0 ? Produto.QuantidadeItensPack : 1;
            }
            //if (!string.IsNullOrWhiteSpace(Quantidade.Value))
            //{
            //int.TryParse(Quantidade.Value, out quantidadeTotalItens);
            quantidadeTotalItens = Quantidade;
            //if (TipoItem == "Caixa")
            //{
            //    quantidadeTotalItens = quantidadeTotalItens * quantidadeItensCaixa;
            //}
            //else 
            if (TipoItem == "Pack")
            {
                quantidadeTotalItens = quantidadeTotalItens * quantidadeItensPack;
            }
            else if (TipoItem == "Unidade")
            {
                //quantidadeTotalItens = quantidadeTotalItens;
            }
            //}

            QuantidadeTotalItens = quantidadeTotalItens;
        }
    }
}
