using KraftSales.Models.Usuarios;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KraftSales.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LoggedUserKey = "logged_user";
        private const string LoggedUserPasswordKey = "passwordKey";
        private const string IdUrlBaseKey = "url_base";
        private const string UrlPortalBaseKey = "url_portal_base";
        private const string DataUltimoDownloadClienteKey = "data_ult_down_cliente";
        private const string DataUltimoDownloadProdutoKey = "data_ult_down_produto";
        private const string DataPrimeiroDownloadProdutoKey = "data_primeiro_down_prod";
        private const string RazaoSocialMaxSizeKey = "razao_social_max";
        private const string NomeFantasiaMaxSizeKey = "nome_fantasia_max";
        private const string ContatoMaxSizeKey = "contato_max";
        private const string EmailMaxSizeKey = "email_max";
        private const string ObservacaoMaxSizeKey = "observacao_max";
        private const string TransporteMaxSizeKey = "transporte_max";
        private const string DescontoMaximoPermitidoKey = "desconto_maximo";
        private const string UserTypeDescriptionKey = "user_type_desc_key";
        private const string RepresentantCodeKey = "representant_key";
        private const string MailHostKey = "mail_host";
        private const string MailPortKey = "mail_port";
        private const string MailUserKey = "mail_user";
        private const string MailPasswordKey = "mail_pwrd";
        private const string MailFromKey = "mail_from";


        private static readonly string LoggedUserDefault = string.Empty;
        private static readonly string LoggedUserPasswordDefault = string.Empty;
        private static readonly string IdUrlBaseDefault = "http://201.55.169.17/wsconsert/wsconsert.asmx/";
        private static readonly string UrlPortalBaseDefault = "http://appfair.kraftcloud.com.br/api/";
        private static readonly string DataUltimoDownloadClienteDefault = string.Empty;
        private static readonly string DataUltimoDownloadProdutoDefault = string.Empty;
        private static readonly string DataPrimeiroDownloadProdutoDefault = "01/01/2017";
        private static readonly int RazaoSocialMaxSizeDefault = 60;
        private static readonly int NomeFantasiaMaxSizeDefault = 40;
        private static readonly int ContatoMaxSizeDefault = 60;
        private static readonly int EmailMaxSizeDefault = 60;
        private static readonly int ObservacaoMaxSizeDefault = 100;
        private static readonly int TransporteMaxSizeDefault = 40;
        private static readonly int DescontoMaximoPermitidoDefault = 10;
        private static readonly string UserTypeDescriptionDefault = "Revendedor";
        private static readonly string RepresentantCodeDefault = string.Empty;
        private static readonly string MailHostDefault = "smtp.gmail.com";
        private static readonly int MailPortDefault = 587;
        private static readonly string MailUserDefault = "dkp.fair@gmail.com";
        private static readonly string MailPasswordDefault = "dkpfair@001";
        private static readonly string MailFromDefault = "noreply.dkpfair@grupodkp.com.br";

        #endregion

        public static string UrlBase
        {
            get => AppSettings.GetValueOrDefault(IdUrlBaseKey, IdUrlBaseDefault);
            set => AppSettings.AddOrUpdateValue(IdUrlBaseKey, value);
        }

        public static string UrlPortalBase
        {
            get => AppSettings.GetValueOrDefault(UrlPortalBaseKey, UrlPortalBaseDefault);
            set => AppSettings.AddOrUpdateValue(UrlPortalBaseKey, value);
        }
        
        public static string LoggedUser
        {
            get => AppSettings.GetValueOrDefault(LoggedUserKey, LoggedUserDefault);
            set => AppSettings.AddOrUpdateValue(LoggedUserKey, value);
        }

        public static string LoggedUserPassword
        {
            get => AppSettings.GetValueOrDefault(LoggedUserPasswordKey, LoggedUserPasswordDefault);
            set => AppSettings.AddOrUpdateValue(LoggedUserPasswordKey, value);
        }

        public static string DataUltimoDownloadCliente
        {
            get => AppSettings.GetValueOrDefault(DataUltimoDownloadClienteKey, DataUltimoDownloadClienteDefault);
            set => AppSettings.AddOrUpdateValue(DataUltimoDownloadClienteKey, value);
        }

        public static string DataUltimoDownloadProduto
        {
            get => AppSettings.GetValueOrDefault(DataUltimoDownloadProdutoKey, DataUltimoDownloadProdutoDefault);
            set => AppSettings.AddOrUpdateValue(DataUltimoDownloadProdutoKey, value);
        }

        public static string DataPrimeiroDownloadProduto
        {
            get => AppSettings.GetValueOrDefault(DataPrimeiroDownloadProdutoKey, DataPrimeiroDownloadProdutoDefault);
            set => AppSettings.AddOrUpdateValue(DataPrimeiroDownloadProdutoKey, value);
        }

        public static int RazaoSocialMaxSize
        {
            get => AppSettings.GetValueOrDefault(RazaoSocialMaxSizeKey, RazaoSocialMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(RazaoSocialMaxSizeKey, value);
        }

        public static int NomeFantasiaMaxSize
        {
            get => AppSettings.GetValueOrDefault(NomeFantasiaMaxSizeKey, NomeFantasiaMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(NomeFantasiaMaxSizeKey, value);
        }

        public static int ContatoMaxSize
        {
            get => AppSettings.GetValueOrDefault(ContatoMaxSizeKey, ContatoMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(ContatoMaxSizeKey, value);
        }

        public static int EmailMaxSize
        {
            get => AppSettings.GetValueOrDefault(EmailMaxSizeKey, EmailMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(EmailMaxSizeKey, value);
        }

        public static int ObservacaoMaxSize
        {
            get => AppSettings.GetValueOrDefault(ObservacaoMaxSizeKey, ObservacaoMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(ObservacaoMaxSizeKey, value);
        }

        public static int TransporteMaxSize
        {
            get => AppSettings.GetValueOrDefault(TransporteMaxSizeKey, TransporteMaxSizeDefault);
            set => AppSettings.AddOrUpdateValue(TransporteMaxSizeKey, value);
        }

        public static int DescontoMaximoPermitido
        {
            get => AppSettings.GetValueOrDefault(DescontoMaximoPermitidoKey, DescontoMaximoPermitidoDefault);
            set => AppSettings.AddOrUpdateValue(DescontoMaximoPermitidoKey, value);
        }
        
        public static string UserTypeDescription
        {
            get => AppSettings.GetValueOrDefault(UserTypeDescriptionKey, UserTypeDescriptionDefault);
            set => AppSettings.AddOrUpdateValue(UserTypeDescriptionKey, value);
        }

        public static string RepresentantCode
        {
            get => AppSettings.GetValueOrDefault(RepresentantCodeKey, RepresentantCodeDefault);
            set => AppSettings.AddOrUpdateValue(RepresentantCodeKey, value);
        }

        public static string MailHost
        {
            get => AppSettings.GetValueOrDefault(MailHostKey, MailHostDefault);
            set => AppSettings.AddOrUpdateValue(MailHostKey, value);
        }

        public static int MailPort
        {
            get => AppSettings.GetValueOrDefault(MailPortKey, MailPortDefault);
            set => AppSettings.AddOrUpdateValue(MailPortKey, value);
        }

        public static string MailUser
        {
            get => AppSettings.GetValueOrDefault(MailUserKey, MailUserDefault);
            set => AppSettings.AddOrUpdateValue(MailUserKey, value);
        }

        public static string MailPassword
        {
            get => AppSettings.GetValueOrDefault(MailPasswordKey, MailPasswordDefault);
            set => AppSettings.AddOrUpdateValue(MailPasswordKey, value);
        }

        public static string MailFrom
        {
            get => AppSettings.GetValueOrDefault(MailFromKey, MailFromDefault);
            set => AppSettings.AddOrUpdateValue(MailFromKey, value);
        }
    }
}
