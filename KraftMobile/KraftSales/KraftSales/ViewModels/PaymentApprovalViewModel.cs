using KraftSales.Extensions;
using KraftSales.Models.Pedidos;
using KraftSales.Validations;
using KraftSales.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class PaymentApprovalViewModel : ViewModelBase
    {
        public ICommand FinishOrderCommand => new Command(async () => await FinishOrderAsync());

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

        //private ValidatableObject<string> _desconto;
        //public ValidatableObject<string> Desconto
        //{
        //    get { return _desconto; }
        //    set
        //    {
        //        _desconto = value;
        //        RaisePropertyChanged(() => Desconto);
        //    }
        //}

        private int _desconto;
        public int Desconto
        {
            get => _desconto;
            set
            {
                _desconto = value;
                RaisePropertyChanged(() => Desconto);
                ReCalculateValorComDesconto();
            }
        }

        private List<string> tiposPagamento = new List<string> { "30/60/90", "45/90" };
        public List<string> TiposPagamento => tiposPagamento;

        private int _tipoPagamentoSelectedIndex;
        public int TipoPagamentoSelectedIndex
        {
            get => _tipoPagamentoSelectedIndex;
            set
            {
                _tipoPagamentoSelectedIndex = value;
                RaisePropertyChanged(() => TipoPagamentoSelectedIndex);
                TipoPagamento = TiposPagamento[_tipoPagamentoSelectedIndex];
            }
        }

        private string _tipoPagamento;
        public string TipoPagamento
        {
            get => _tipoPagamento;
            set
            {
                _tipoPagamento = value;
                RaisePropertyChanged(() => TipoPagamento);
            }
        }


        private List<string> representantes = new List<string> { "Camilo", "Patricia", "Paula", "Roberta" };
        public List<string> Representantes => representantes;

        private int _representanteSelectedIndex;
        public int RepresentanteSelectedIndex
        {
            get => _representanteSelectedIndex;
            set
            {
                _representanteSelectedIndex = value;
                RaisePropertyChanged(() => RepresentanteSelectedIndex);
                Representante = Representantes[_tipoPagamentoSelectedIndex];
            }
        }

        private string _representante;
        public string Representante
        {
            get => _representante;
            set
            {
                _representante = value;
                RaisePropertyChanged(() => Representante);
            }
        }

        public PaymentApprovalViewModel()
        {
            _pedido = new Pedido();
            ItensPedidos = new ObservableCollection<ItemPedido>();
            //Desconto = new ValidatableObject<string>();

            AddValidations();
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Pedido pedido)
            {
                _pedido = pedido;
                ItensPedidos = _pedido.Itens.ToObservableCollection();
                ValorTotal = _pedido.ValorTotal;
                TotalItens = _pedido.Itens.Sum(c => c.Quantidade);
                ValorTotalComDesconto = (100.0 - pedido.Desconto) * _pedido.ValorTotal / 100;
                //Desconto.Value = _pedido.Desconto.ToString();
                Desconto = Convert.ToInt32(_pedido.Desconto);
            }

            return base.InitializeAsync(navigationData);
        }

        private async Task FinishOrderAsync()
        {
            IsBusy = true;

            if (await DialogService.ConfirmAsync("Confirma o pedido?", "", "OK", "Cancelar"))
            {
                await Task.Delay(200);

                //_pedido.DataEntrega = DataEntrega;
                //_pedido.TipoFrete = TipoFrete.Codigo;
                //_pedido.Observacao = Observacao.Value.ToUpper();
                //_pedido.Representante = Representante.Codigo;
                //_pedido.TipoPagamentoId = TipoPagamento.Codigo;
                //_pedido.RepresentanteInfo = Representante;

                //if (_pedido.TipoFrete == "FOB")
                //{
                //    if (string.IsNullOrWhiteSpace(Transporte.Value))
                //    {
                //        await DialogService.ShowAlertAsync("Para o frete tipo FOB o campo transporte é obrigatório", "", "OK");
                //        IsBusy = false;
                //        return;
                //    }

                //    _pedido.Transporte = Transporte.Value.ToUpper();
                //}
                //else
                //{
                //    _pedido.Transporte = string.Empty;
                //}

                ////int.TryParse(Desconto.Value, out int desconto);
                ////int desconto = Desconto;
                //_pedido.Desconto = Desconto;

                if (await DialogService.ConfirmAsync("Deseja imprimir 3 vias do pedido?", "", "Sim", "Não"))
                {
                    await NavigationService.NavigateToAsync<PrintOrderViewModel>(_pedido);
                }
                else
                {
                    DialogService.Toast("Pedido realizado com sucesso", 5);
                    await NavigationService.GoToRootAsync();
                }


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
    }
}
