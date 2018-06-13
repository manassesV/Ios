using KraftSales.ViewModels.Base;
using System.Threading.Tasks;

namespace KraftSales.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        ViewModelBase CurrentPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task GoBackAsync();

        Task CloseModalAsync();

        Task GoToRootAsync();
    }
}
