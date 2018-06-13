using KraftSales.Helpers;
using KraftSales.Models.Usuarios;
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
    public class LoginViewModel : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private bool _isValid;
        private bool _isLogin;
        private string _authUrl;

        private IUsuarioService _usuarioService;

        public LoginViewModel(IUsuarioService usuarioService)
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
        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());


        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is LogoutParameter)
            {
                var logoutParameter = (LogoutParameter)navigationData;

                if (logoutParameter.Logout)
                {
                    Logout();
                }
            }

            return base.InitializeAsync(navigationData);
        }

        private async Task MockSignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            Usuario user = null;

            if (isValid)
            {
                try
                {
                    user = await _usuarioService.AuthUser(UserName.Value, Password.Value);

                    if (!string.IsNullOrWhiteSpace(user.MensagemRetornoAutenticacao))
                    {
                        await DialogService.ShowAlertAsync(user.MensagemRetornoAutenticacao, "", "OK");
                    }
                    else
                    {
                        Settings.LoggedUser = user.Login.ToLower();
                        Settings.UserTypeDescription = user.TipoUsuario;
                        Settings.RepresentantCode = user.CodigoRepresentante;
                        Settings.LoggedUserPassword = _password.Value;
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
                await NavigationService.NavigateToAsync<MainViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();
            }

            IsBusy = false;
        }

        private void Logout()
        {
            Settings.LoggedUser = string.Empty;
        }

        private async Task SettingsAsync()
        {
            await Task.FromResult(0);
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
