using KraftSales.Extensions;
using KraftSales.Helpers;
using KraftSales.Models.Pedidos;
using KraftSales.Services.EnvioEmails;
using KraftSales.Services.Pedidos;
using KraftSales.Services.Representantes;
using KraftSales.Services.TipoFretes;
using KraftSales.Services.TipoPagamentos;
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
    public class ApprovalViewModel : ViewModelBase
    {
        public ICommand ApproveCommand => new Command(async () => await ApproveAsync());
        public ICommand RejectCommand => new Command(async () => await RejectAsync());
        public ICommand ValidateTransporteCommand => new Command(() => ValidateTransporte());
        public ICommand ValidateObservacaoCommand => new Command(() => ValidateObservacao());

        private Pedido _pedido;

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

        private double _valorTotal;
        public double ValorTotal
        {
            get => _valorTotal;
            set
            {
                _valorTotal = value;
                RaisePropertyChanged(() => ValorTotal);
            }
        }

        private int _totalItens;
        public int TotalItens
        {
            get => _totalItens;
            set
            {
                _totalItens = value;
                RaisePropertyChanged(() => TotalItens);
            }
        }

        private double _valorComDesconto;
        public double ValorTotalComDesconto
        {
            get => _valorComDesconto;
            set
            {
                _valorComDesconto = value;
                RaisePropertyChanged(() => ValorTotalComDesconto);
            }
        }

        private int _desconto;
        public int Desconto
        {
            get => _desconto;
            set
            {
                _desconto = value;
                RaisePropertyChanged(() => Desconto);
                ReCalculateValorComDesconto();
                ReCalculateTipoFrete();
            }
        }

        private DateTime _dataEntrega;
        public DateTime DataEntrega
        {
            get => _dataEntrega;
            set
            {
                _dataEntrega = value;
                RaisePropertyChanged(() => DataEntrega);
            }
        }

        private DateTime _dataEntregaMinima;
        public DateTime DataEntregaMinima
        {
            get => _dataEntregaMinima;
            set
            {
                _dataEntregaMinima = value;
                RaisePropertyChanged(() => DataEntregaMinima);
            }
        }

        private DateTime _dataEntregaMaxima;
        public DateTime DataEntregaMaxima
        {
            get => _dataEntregaMaxima;
            set
            {
                _dataEntregaMaxima = value;
                RaisePropertyChanged(() => DataEntregaMaxima);
            }
        }

        private ObservableCollection<TipoPagamento> tiposPagamento;
        public ObservableCollection<TipoPagamento> TiposPagamento
        {
            get { return tiposPagamento; }
            set
            {
                tiposPagamento = value;
                RaisePropertyChanged(() => TiposPagamento);
            }
        }

        private TipoPagamento _tipoPagamento;
        public TipoPagamento TipoPagamento
        {
            get => _tipoPagamento;
            set
            {
                _tipoPagamento = value;
                RaisePropertyChanged(() => TipoPagamento);
            }
        }

        private ObservableCollection<TipoFrete> tiposFrete;
        public ObservableCollection<TipoFrete> TiposFrete
        {
            get { return tiposFrete; }
            set
            {
                tiposFrete = value;
                RaisePropertyChanged(() => TiposFrete);
            }
        }

        private TipoFrete _tipoFrete;
        public TipoFrete TipoFrete
        {
            get => _tipoFrete;
            set
            {
                _tipoFrete = value;
                RaisePropertyChanged(() => TipoFrete);
                if (_tipoFrete != null)
                {
                    ExibeTransporte = _tipoFrete.Codigo == "FOB";
                }
            }
        }

        private ValidatableObject<string> _observacao;
        public ValidatableObject<string> Observacao
        {
            get => _observacao;
            set
            {
                _observacao = value;
                RaisePropertyChanged(() => Observacao);
            }
        }

        private ObservableCollection<Representante> representantes;
        public ObservableCollection<Representante> Representantes
        {
            get { return representantes; }
            set
            {
                representantes = value;
                RaisePropertyChanged(() => Representantes);
            }
        }

        private Representante _representante;
        public Representante Representante
        {
            get => _representante;
            set
            {
                _representante = value;
                RaisePropertyChanged(() => Representante);
            }
        }

        private ValidatableObject<string> _transporte;
        public ValidatableObject<string> Transporte
        {
            get => _transporte;
            set
            {
                _transporte = value;
                RaisePropertyChanged(() => Transporte);
            }
        }

        private bool _exibeTransporte;
        public bool ExibeTransporte
        {
            get => _exibeTransporte;
            set
            {
                _exibeTransporte = value;
                RaisePropertyChanged(() => ExibeTransporte);
            }
        }

        private IPedidoService _pedidoService;
        private ITipoFreteService _tipoFreteService;
        private ITipoPagamentoService _tipoPagamentoService;
        private IRepresentanteService _representanteService;
        private IEnvioEmailService _envioEmailService;

        public ApprovalViewModel(ITipoFreteService tipoFreteService, ITipoPagamentoService tipoPagamentoService, IRepresentanteService representanteService,
                                 IPedidoService pedidoService)
        {
            _tipoFreteService = tipoFreteService;
            _tipoPagamentoService = tipoPagamentoService;
            _representanteService = representanteService;
            _pedidoService = pedidoService;
            _envioEmailService = DependencyService.Get<IEnvioEmailService>();

            _pedido = new Pedido();
            ItensPedidos = new ObservableCollection<ItemPedido>();
            //Desconto = new ValidatableObject<string>();
            Transporte = new ValidatableObject<string>();
            Observacao = new ValidatableObject<string>();

            DataEntrega = DateTime.Now.AddDays(3);
            DataEntregaMinima = DateTime.Now.AddDays(1);
            DataEntregaMaxima = DateTime.Now.AddYears(2);

            AddValidations();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await LoadTiposFreteAsync();
                await LoadTiposPagamentoAsync();
                await LoadRepresentantesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (navigationData is Pedido pedido)
            {
                try
                {
                    _pedido = pedido;
                    ItensPedidos = _pedido.Itens.ToObservableCollection();
                    ValorTotal = _pedido.ValorTotal;
                    TotalItens = _pedido.Itens.Sum(c => c.Quantidade);
                    Desconto = pedido.Desconto;
                    ValorTotalComDesconto = (100.0 - pedido.Desconto) * _pedido.ValorTotal / 100;
                    Observacao.Value = pedido.Observacao;
                    Transporte.Value = pedido.Transporte;

                    TipoPagamento = TiposPagamento.FirstOrDefault(c => c.Codigo == pedido.TipoPagamentoId);
                    Representante = Representantes.FirstOrDefault(c => c.Codigo == pedido.Representante);
                    TipoFrete = TiposFrete.FirstOrDefault(c => c.Codigo == pedido.TipoFrete);

                    DataEntrega = pedido.DataEntrega;
                    DataEntregaMinima = pedido.DataPedido.AddDays(1);
                    DataEntregaMaxima = pedido.DataPedido.AddYears(2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private async Task LoadTiposFreteAsync()
        {
            if (TiposFrete == null || !TiposFrete.Any())
            {
                TiposFrete = await _tipoFreteService.GetAllAsync();
            }
        }

        private async Task LoadTiposPagamentoAsync()
        {
            if (TiposPagamento == null || !TiposPagamento.Any())
            {
                TiposPagamento = await _tipoPagamentoService.GetAllAsync();
            }
        }

        private async Task LoadRepresentantesAsync()
        {
            if (Representantes == null || !Representantes.Any())
            {
                Representantes = await _representanteService.GetAllOfflineAsync();
            }
        }

        public async Task ApproveAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DialogService.ShowAlertAsync(Constants.MENSAGEM_SEM_CONEXAO, "", "OK");
                return;
            }

            if (await DialogService.ConfirmAsync("Confirma o pedido?", "", "OK", "Cancelar"))
            {
                IsBusy = true;

                try
                {
                    _pedido.Desconto = Desconto;
                    _pedido.TipoPagamentoId = TipoPagamento?.Codigo;
                    _pedido.Representante = Representante?.Codigo;
                    _pedido.RepresentanteInfo = Representante;
                    _pedido.DataEntrega = DataEntrega;
                    _pedido.TipoFrete = TipoFrete?.Codigo;
                    _pedido.Observacao = string.IsNullOrEmpty(Observacao.Value) ? string.Empty : Observacao.Value.ToUpper();
                    _pedido.ValorComDesconto = ValorTotalComDesconto;

                    if (_pedido.TipoFrete == "FOB")
                    {
                        if (string.IsNullOrWhiteSpace(Transporte.Value))
                        {
                            await DialogService.ShowAlertAsync("Para o frete tipo FOB o campo Transportadora é obrigatório", "", "OK");
                            IsBusy = false;
                            return;
                        }

                        _pedido.Transporte = Transporte.Value.ToUpper();
                    }
                    else
                    {
                        _pedido.Transporte = string.Empty;
                    }

                    //if (await DialogService.ConfirmAsync("Deseja imprimir 3 vias do pedido?", "", "Sim", "Não"))
                    //{
                    //    await NavigationService.NavigateToAsync<PrintOrderViewModel>(_pedido);
                    //}
                    //else
                    //{
                    var retornoPedido = await _pedidoService.SendOrder(_pedido);
                    string tipoPedido = _pedido.PedidoDeClienteNovo ? "Orçamento" : "Pedido";

                    if (retornoPedido == null)
                    {
                        await DialogService.ShowAlertAsync($"Retorno com erro do servidor", $"Erro ao enviar {tipoPedido}", "OK");
                    }
                    else if (retornoPedido.pedido.retorno.Equals("cadastro ok", StringComparison.CurrentCultureIgnoreCase) ||
                                 retornoPedido.pedido.retorno.ToUpper().Contains("COM SUCESSO"))
                    {
                        _pedido.NumeroPedido = retornoPedido.pedido.codigoMK;
                        await DialogService.ShowAlertAsync($"Número do {tipoPedido} gerado: {retornoPedido.pedido.codigoMK}", $"{tipoPedido} realizado com sucesso", "OK");
                        await SendMail();

                        DialogService.Toast($"{tipoPedido} {retornoPedido.pedido.codigoMK} realizado com sucesso", 5);
                        await NavigationService.GoToRootAsync();
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync($"Detalhes do erro: {retornoPedido.pedido.retorno}", $"Erro ao enviar {tipoPedido}", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                //}
            }

            IsBusy = false;
        }

        private async Task SendMail()
        {
            try
            {
                string to = string.Empty;
                string subject = $"Mochine - Resumo do pedido {_pedido.NumeroPedido}";
                string body = MailHelper.GenerateMailBody(_pedido);
                var ccs = new List<string>();

                if (!string.IsNullOrWhiteSpace(_pedido.Cliente.CliEndEle))
                {
                    to = _pedido.Cliente.CliEndEle;
                    ccs.Add(Settings.LoggedUser);
                }
                else
                {
                    to = Settings.LoggedUser;
                }

                ccs.Add("luizsaga.mochine@gmail.com");
                ccs.Add("ariel@mochine.com.br");

                await _envioEmailService.SendMail(to, Settings.MailFrom, subject, body, ccs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao enviar pedido por email{ex}");
            }
        }

        public async Task RejectAsync()
        {
            IsBusy = true;

            if (await DialogService.ConfirmAsync("Deseja mesmo rejeitar o pedido?", "", "Sim", "Não"))
            {
                DialogService.Toast("Pedido rejeitado", 5);

                MessagingCenter.Send(this, MessageKeys.OrderRejected, _pedido);

                await NavigationService.GoBackAsync();
            }

            IsBusy = false;
        }

        private void AddValidations()
        {
            //_desconto.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Insira o desconto" });
        }

        private void ReCalculateValorComDesconto()
        {
            double valorComDesconto = 0.0;

            if (ValorTotal > 0.0)
            {
                valorComDesconto = (100.0 - Desconto) * ValorTotal / 100;
            }

            ValorTotalComDesconto = valorComDesconto;
        }

        private void ReCalculateTipoFrete()
        {
            if (TiposFrete != null && TiposFrete.Any())
            {
                if (ValorTotalComDesconto < 5000)
                {
                    TipoFrete = TiposFrete.FirstOrDefault(c => c.Codigo == "FOB");
                }
                else
                {
                    TipoFrete = TiposFrete.FirstOrDefault(c => c.Codigo == "CIF");
                }
            }
        }

        private bool ValidateTransporte()
        {
            if (!string.IsNullOrWhiteSpace(_transporte.Value))
            {
                var text = _transporte.Value;
                var maxSize = Settings.TransporteMaxSize;

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_transporte.Value != text)
                {
                    _transporte.Value = text;
                }
            }

            return true;
        }

        private bool ValidateObservacao()
        {
            if (!string.IsNullOrWhiteSpace(_observacao.Value))
            {
                var text = _observacao.Value;
                var maxSize = Settings.ObservacaoMaxSize;

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_observacao.Value != text)
                {
                    _observacao.Value = text;
                }
            }

            return true;
        }

    }
}
