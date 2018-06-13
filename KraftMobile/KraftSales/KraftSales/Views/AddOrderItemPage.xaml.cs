using KraftSales.ViewModels;
using KraftSales.ViewModels.Base;
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
    public partial class AddOrderItemPage : ContentPage
    {
        bool firstLoad = true;

        public AddOrderItemPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (firstLoad)
            {
                MessagingCenter.Unsubscribe<AddOrderItemViewModel>(this, MessageKeys.OrderItemGraphLoaded);
                MessagingCenter.Subscribe<AddOrderItemViewModel>(this, MessageKeys.OrderItemGraphLoaded, (sender) =>
                {
                    gridPack.SortedColumnIndex = 0;
                    //gridCaixa.SortedColumnIndex = 0;
                });

                firstLoad = false;
            }
        }
    }
}