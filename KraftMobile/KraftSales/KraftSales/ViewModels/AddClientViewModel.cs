using KraftSales.Helpers;
using KraftSales.Models.Clientes;
using KraftSales.Services.Clientes;
using KraftSales.Validations;
using KraftSales.ViewModels.Base;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class AddClientViewModel : ViewModelBase
    {
        public ICommand SaveCommand => new Command(async () => await SaveAsync());
        public ICommand ValidateCnpjCommand => new Command(() => ValidateCnpj());
        public ICommand ValidateRazaoSocialCommand => new Command(() => ValidateRazaoSocial());
        public ICommand ValidateNomeFantasiaCommand => new Command(() => ValidateNomeFantasia());
        public ICommand ValidateNomeContatoCommand => new Command(() => ValidateNomeContato());
        public ICommand ValidateTelefoneContatoCommand => new Command(() => ValidateTelefoneContato());
        public ICommand ValidateEmailContatoCommand => new Command(() => ValidateEmailContato());

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

        private ValidatableObject<string> _nomeContato;
        public ValidatableObject<string> NomeContato
        {
            get => _nomeContato;
            set
            {
                _nomeContato = value;
                RaisePropertyChanged(() => NomeContato);
            }
        }

        private ValidatableObject<string> _emailContato;
        public ValidatableObject<string> EmailContato
        {
            get => _emailContato;
            set
            {
                _emailContato = value;
                RaisePropertyChanged(() => EmailContato);
            }
        }

        private ValidatableObject<string> _telefoneContato;
        public ValidatableObject<string> TelefoneContato
        {
            get => _telefoneContato;
            set
            {
                _telefoneContato = value;
                RaisePropertyChanged(() => TelefoneContato);
            }
        }

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
                _cnpj.Value = string.Empty;
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


        private IClienteService _clienteService;

        public AddClientViewModel(IClienteService clienteService)
        {
            _clienteService = clienteService;

            _cnpj = new ValidatableObject<string>();
            _razaoSocial = new ValidatableObject<string>();
            _nomeFantasia = new ValidatableObject<string>();
            _nomeContato = new ValidatableObject<string>();
            _emailContato = new ValidatableObject<string>();
            _telefoneContato = new ValidatableObject<string>();

            AddValidations();
        }

        private async Task SaveAsync()
        {
            IsBusy = true;
            IsValid = true;

            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    var cliente = new Cliente
                    {
                        Cnpj = Regex.Replace(_cnpj.Value, @"[^0-9]", ""),
                        EhNovo = true,
                        DataUltimaCompra = null,
                        RazaoSocial = RazaoSocial.Value.ToUpper(),
                        NomeFantasia = NomeFantasia.Value.ToUpper(),
                        Ativo = true,
                        EmailContato = EmailContato.Value.ToUpper(),
                        NomeContato = NomeContato.Value.ToUpper(),
                        TelefoneContato = TelefoneContato.Value,
                        TipoCliente = PessoaJuridica ? Enums.TipoCliente.PessoaJuridica : Enums.TipoCliente.PessoaFisica,
                    };

                    var retorno = await _clienteService.RegisterNewClient(cliente);

                    if (retorno == null)
                    {
                        await DialogService.ShowAlertAsync($"Retorno do Portal com erro", "Erro ao cadastrar cliente", "OK");
                    }
                    else if (retorno.cliente.retorno.ToUpper().Contains("COM SUCESSO"))
                    {
                        DialogService.Toast($"Cliente cadastrado com sucesso", 5);

                        //DialogService.ShowSuccess($"Cliente cadastrado com sucesso", 5);

                        if (Settings.RepresentantCode.ToLowerInvariant() == "" || Settings.RepresentantCode.ToLowerInvariant() == "0")
                        {
                            await NavigationService.GoBackAsync();
                        }
                        else
                        {
                            await NavigationService.NavigateToAsync<OrderViewModel>(cliente);
                        }
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync($"Detalhes do erro: {retorno.cliente.retorno}", "Erro ao cadastrar cliente", "OK");
                    }

                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine($"[SaveAsync] Error => {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            IsBusy = false;
        }

        private bool Validate()
        {
            bool isValidCnpj = ValidateCnpj();

            if (isValidCnpj)
            {
                isValidCnpj = ValidateCodigoCliente();
            }

            bool isValidRazaoSocial = ValidateRazaoSocial();
            bool isValidNomeFantasia = ValidateNomeFantasia();
            bool isValidNomeContato = ValidateNomeContato();
            bool isValidEmailContato = ValidateEmailContato();
            bool isValidTelefone = ValidateTelefoneContato();

            return isValidCnpj && isValidRazaoSocial && isValidNomeFantasia &&
                    isValidNomeContato && isValidEmailContato && isValidTelefone;
        }

        private bool ValidateCodigoCliente()
        {
            if (PessoaJuridica)
            {
                var isValid = ValidaCnpj.Check(_cnpj.Value);
                if (!isValid)
                {
                    _cnpj.Errors.Add("CNPJ inválido");
                }
                return isValid;
            }
            else
            {
                var isValid = ValidaCpf.Check(_cnpj.Value);
                if (!isValid)
                {
                    _cnpj.Errors.Add("CPF inválido");
                }
                return isValid;
            }
        }

        private bool ValidateCnpj()
        {
            var cnpjExistente = false;
            var msgErro = string.Empty;

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

                    if (text.Length == 18)
                    {
                        var cliente = _clienteService.GetClienteAsync(text).Result;
                        if (cliente != null)
                        {
                            cnpjExistente = true;
                            msgErro = "Já existe um cliente cadastrado com este CNPJ";
                        }
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

                    if (text.Length == 14)
                    {
                        var cliente = _clienteService.GetClienteAsync(text).Result;
                        if (cliente != null)
                        {
                            cnpjExistente = true;
                            msgErro = "Já existe um cliente cadastrado com este CPF";
                        }
                    }
                }

                if (_cnpj.Value != text)
                {
                    _cnpj.Value = text;
                }
            }

            if (cnpjExistente)
            {
                Cnpj.IsValid = false;
                Cnpj.Errors = new List<string> { msgErro };
                return false;
            }

            return _cnpj.Validate();
        }

        private bool ValidateRazaoSocial()
        {
            if (!string.IsNullOrWhiteSpace(_razaoSocial.Value))
            {
                var text = _razaoSocial.Value;
                var maxSize = Settings.RazaoSocialMaxSize;
                //text = text.PadRight(maxSize);

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_razaoSocial.Value != text)
                {
                    _razaoSocial.Value = text;
                }
            }

            return _razaoSocial.Validate();
        }

        private bool ValidateNomeFantasia()
        {
            if (!string.IsNullOrWhiteSpace(_nomeFantasia.Value))
            {
                var text = _nomeFantasia.Value;
                var maxSize = Settings.NomeFantasiaMaxSize;
                //text = text.PadRight(maxSize);

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_nomeFantasia.Value != text)
                {
                    _nomeFantasia.Value = text;
                }
            }

            return _nomeFantasia.Validate();
        }

        private bool ValidateNomeContato()
        {
            if (!string.IsNullOrWhiteSpace(_nomeContato.Value))
            {
                var text = _nomeContato.Value;
                var maxSize = Settings.ContatoMaxSize;
                //text = text.PadRight(maxSize);

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_nomeContato.Value != text)
                {
                    _nomeContato.Value = text;
                }
            }

            return _nomeContato.Validate();
        }

        private bool ValidateEmailContato()
        {
            if (!string.IsNullOrWhiteSpace(_emailContato.Value))
            {
                var text = _emailContato.Value;
                var maxSize = Settings.EmailMaxSize;
                //text = text.PadRight(maxSize);

                if (text.Length > maxSize)
                {
                    text = text.Remove(maxSize);
                }

                if (_emailContato.Value != text)
                {
                    _emailContato.Value = text;
                }
            }

            return _emailContato.Validate();
        }

        private bool ValidateTelefoneContato()
        {
            if (!string.IsNullOrWhiteSpace(_telefoneContato.Value))
            {
                string text = Regex.Replace(_telefoneContato.Value, @"[^0-9]", "");

                text = text.PadRight(11);

                if (text.Length > 11)
                {
                    text = text.Remove(11);
                }

                text = text.Insert(0, "(").Insert(3, ")").TrimEnd(new char[] { ' ', '(', ')' });

                if (_telefoneContato.Value != text)
                {
                    _telefoneContato.Value = text;
                }
            }

            return _telefoneContato.Validate();
        }

        private void AddValidations()
        {
            _cnpj.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _razaoSocial.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _nomeFantasia.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _nomeContato.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
            _emailContato.Validations.Add(new EmailValidationRule<string> { ValidationMessage = "E-mail inválido" });
            _telefoneContato.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Campo obrigatório" });
        }
    }
}
