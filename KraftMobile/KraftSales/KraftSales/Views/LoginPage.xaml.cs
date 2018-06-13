using KraftSales.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool _animate;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override void OnAppearing()
        {
            var content = Content;
            Content = null;
            Content = content;

            var vm = BindingContext as LoginViewModel;
            if (vm != null)
            {
                _animate = true;
            }
        }

        protected override void OnDisappearing()
        {
            _animate = false;
        }

        //public async Task AnimateIn()
        //{
        //    if (Device.RuntimePlatform == Device.Windows)
        //    {
        //        return;
        //    }

        //    await AnimateItem(Banner, 1000);
        //}

        //private async Task AnimateItem(View uiElement, uint duration)
        //{
        //    try
        //    {
        //        while (_animate)
        //        {
        //            await uiElement.ScaleTo(1.05, duration, Easing.SinInOut);
        //            await Task.WhenAll(
        //                uiElement.FadeTo(1, duration, Easing.SinInOut),
        //                //uiElement.LayoutTo(new Rectangle(new Point(0, 0), new Size(uiElement.Width, uiElement.Height))),
        //                uiElement.FadeTo(.9, duration, Easing.SinInOut),
        //                uiElement.ScaleTo(1.8, duration, Easing.SinInOut)
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
    }
}