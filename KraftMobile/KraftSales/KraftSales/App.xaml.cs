using KraftSales.Helpers;
using KraftSales.Realms;
using KraftSales.Services;
using KraftSales.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KraftSales
{
    public partial class App : Application
    {
        private static RealmContext _realmContext;
        public static RealmContext RealmContext => _realmContext ?? (_realmContext = new RealmContext());

        public string UrlBase => Settings.UrlBase;
        public static int MailPort => Settings.MailPort;
        public static string MailHost => Settings.MailHost;
        public static string MailUser => Settings.MailUser;
        public static string MailPassword => Settings.MailPassword;

        public App()
        {
            InitializeComponent();
            InitApp();
        }

        private void InitApp()
        {
            Settings.UrlBase = @"http://201.55.169.17/wsconsert/wsconsert.asmx/";
            ViewModelLocator.RegisterDependencies();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
    }
}
