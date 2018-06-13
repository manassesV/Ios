
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentPage : ContentPage
	{
		public PaymentPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}