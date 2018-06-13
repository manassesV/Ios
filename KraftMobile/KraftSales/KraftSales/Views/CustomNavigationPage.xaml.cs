
using Xamarin.Forms;

namespace KraftSales.Views
{
    public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}