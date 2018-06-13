using KraftSales.ViewModels.Base;
using System.Threading.Tasks;

namespace KraftSales.ViewModels
{
    public class ExtendedSplashViewModel : ViewModelBase
    {
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await NavigationService.InitializeAsync();

            IsBusy = false;
        }
    }
}
