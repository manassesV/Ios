using KraftSales.Models.Produtos;
using KraftSales.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KraftSales.ViewModels
{
    public class ProductImageViewModel : ViewModelBase
    {
        private ObservableCollection<Photo> _photos = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                _photos = value;
                RaisePropertyChanged(() => Photos);
            }
        }

        private string _descricaoProduto;
        public string DescricaoProduto
        {
            get => _descricaoProduto;
            set
            {
                _descricaoProduto = value;
                RaisePropertyChanged(() => DescricaoProduto);
            }
        }
        
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Produto produto)
            {
                DescricaoProduto = produto.Descricao;
                Photos = new ObservableCollection<Photo> { new Photo { Url = produto.ImagemUrl } };
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
