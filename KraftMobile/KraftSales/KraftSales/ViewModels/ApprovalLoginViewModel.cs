using KraftSales.Helpers;
using KraftSales.Models.Pedidos;
using KraftSales.Models.Usuarios;
using KraftSales.Services.Pedidos;
using KraftSales.Services.Usuarios;
using KraftSales.Validations;
using KraftSales.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class ApprovalLoginViewModel : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private bool _isValid;
        private bool _isLogin;
        private string _authUrl;

        private IUsuarioService _usuarioService;

        public ApprovalLoginViewModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private Pedido _pedido;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        public string LoginUrl
        {
            get
            {
                return _authUrl;
            }
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => LoginUrl);
            }
        }

        public ICommand MockSignInCommand => new Command(async () => await MockSignInAsync());
        public ICommand EnviarFilaAprovacaoCommand => new Command(async () => await EnviarFilaAprovacaoAsync());
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Pedido pedido)
            {
                _pedido = pedido;
            }

            UserName.Value = "ariel@mochine.com.br";

            return base.InitializeAsync(navigationData);
        }

        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    //var user = await _usuarioService.GetAsync(UserName.Value);

                    //if (user == null || !user.Senha.Equals(Password.Value))
                    //{
                    //    await DialogService.ShowAlertAsync("Usuário e/ou senha incorretos", "", "OK");
                    //}
                    //else if (user.TipoUsuario.ToLowerInvariant() != TipoUsuario.Approver.ToLowerInvariant())
                    //{
                    //    await DialogService.ShowAlertAsync("Somente usuários com permissão de Aprovador podem executar a operação", "", "OK");
                    //}
                    //else
                    //{
                    //    isAuthenticated = true;
                    //}

                    var user = await _usuarioService.AuthUser(UserName.Value, Password.Value);

                    if (!string.IsNullOrWhiteSpace(user.MensagemRetornoAutenticacao))
                    {
                        await DialogService.ShowAlertAsync(user.MensagemRetornoAutenticacao, "", "OK");
                    }
                    else if (user.TipoUsuario.ToLowerInvariant() != TipoUsuario.Approver.ToLowerInvariant())
                    {
                        await DialogService.ShowAlertAsync("Somente usuários com permissão de Aprovador podem executar a operação", "", "OK");
                    }
                    else
                    {
                        isAuthenticated = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            if (isAuthenticated)
            {
                await NavigationService.NavigateToAsync<ApprovalViewModel>(_pedido);
                await NavigationService.RemoveLastFromBackStackAsync();
            }

            IsBusy = false;
        }

        private async Task EnviarFilaAprovacaoAsync()
        {
            IsBusy = true;

            if (await DialogService.ConfirmAsync("Deseja imprimir 3 vias do pedido?", "", "Sim", "Não"))
            {
                await NavigationService.NavigateToAsync<PrintOrderViewModel>(_pedido);
            }
            else
            {
                DialogService.Toast("Pedido enviado para fila de aprovação", 5);
                await NavigationService.GoToRootAsync();
            }

            IsBusy = false;
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Insira seu usuário" });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Insira sua senha" });
        }
    }
}
