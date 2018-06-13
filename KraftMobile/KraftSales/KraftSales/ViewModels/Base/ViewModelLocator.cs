using Autofac;
using KraftSales.Services;
using KraftSales.Services.Clientes;
using KraftSales.Services.Grupos;
using KraftSales.Services.Impressoras;
using KraftSales.Services.Pedidos;
using KraftSales.Services.Produtos;
using KraftSales.Services.Representantes;
using KraftSales.Services.TipoClientes;
using KraftSales.Services.TipoFretes;
using KraftSales.Services.TipoPagamentos;
using KraftSales.Services.Usuarios;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace KraftSales.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // View models
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<AddClientViewModel>();
            builder.RegisterType<ApprovalViewModel>();
            builder.RegisterType<OrderViewModel>();
            builder.RegisterType<AddOrderItemViewModel>();
            builder.RegisterType<PaymentViewModel>();
            builder.RegisterType<PrintOrderViewModel>();
            builder.RegisterType<OrdersToApproveViewModel>();
            builder.RegisterType<PaymentApprovalViewModel>();
            builder.RegisterType<ServiceTestViewModel>();
            builder.RegisterType<ApprovalLoginViewModel>();
            builder.RegisterType<ProductImageViewModel>();
            // Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();

            builder.RegisterInstance(new ImpressoraMockService()).As<IImpressoraService>();
            builder.RegisterInstance(new RepresentanteService()).As<IRepresentanteService>();
            builder.RegisterInstance(new TipoClienteService()).As<ITipoClienteService>();
            builder.RegisterInstance(new TipoFreteService()).As<ITipoFreteService>();
            builder.RegisterInstance(new TipoPagamentoService()).As<ITipoPagamentoService>();
            builder.RegisterInstance(new ProdutoService()).As<IProdutoService>();
            builder.RegisterInstance(new PedidoService()).As<IPedidoService>();
            builder.RegisterInstance(new UsuarioService()).As<IUsuarioService>();

            builder.RegisterType<GrupoService>().As<IGrupoService>().SingleInstance();
            builder.RegisterType<ClienteService>().As<IClienteService>().SingleInstance();

            if (_container != null)
            {
                _container.Dispose();
            }
            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            viewName = viewName.Remove(viewName.LastIndexOf("Page"));
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}