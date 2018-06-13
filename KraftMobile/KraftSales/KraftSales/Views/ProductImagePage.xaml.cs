using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductImagePage : ContentPage
    {
        public ProductImagePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}