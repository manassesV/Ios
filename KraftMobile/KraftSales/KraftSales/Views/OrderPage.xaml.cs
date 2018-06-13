using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            NavigationPage.SetHasBackButton(this, false);
        }
        
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("ATENÇÃO!", "Você deseja mesmo cancelar o pedido?", "Sim", "Não");
                if (result)
                    await Navigation.PopAsync(); // or anything else         
            });

            return true;
        }
        
    }
}