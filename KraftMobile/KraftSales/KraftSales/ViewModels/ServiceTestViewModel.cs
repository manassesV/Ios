using KraftSales.Models.Produtos;
using KraftSales.Services.Grupos;
using KraftSales.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KraftSales.ViewModels
{
    public class ServiceTestViewModel : ViewModelBase
    {
        //public ICommand GetClientsCommand => new Command(async () => await GetClientsAsync());
        //public ICommand GetDetailsCommand => new Command(async () => await GetDetailsAsync());
        public ICommand GetGroupsCommand => new Command(async () => await GetGroupsAsync());
        //public ICommand GetProductsCommand => new Command(async () => await GetProductsAsync());
        private IGrupoService _grupoService;

        private ObservableCollection<Grupo> _grupos;
        public ObservableCollection<Grupo> Grupos
        {
            get => _grupos;
            set
            {
                _grupos = value;
                RaisePropertyChanged(() => Grupos);
            }
        }

        public ServiceTestViewModel(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }
        
        private async Task GetGroupsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Grupos = await _grupoService.GetAllGroupsAsync();

            IsBusy = false;
        }

    }
}
