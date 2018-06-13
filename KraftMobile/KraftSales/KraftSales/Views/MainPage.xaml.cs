using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        //private bool _animate;

        double _width = 0;
        double _height = 0;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;
                ChangeOrientation();
            }
        }

        void ChangeOrientation()
        {

            //gridButtons.RowDefinitions.Clear();
            //gridButtons.ColumnDefinitions.Clear();

            //if (_width > _height) //Paisagem
            //{
            //    rowPlantas.Height = new GridLength(1, GridUnitType.Auto);
            //    //rowPlantas.Height = new GridLength(.1, GridUnitType.Star);
            //    rowCarousel.Height = new GridLength(.6, GridUnitType.Star);
            //    rowButtons.Height = new GridLength(.3, GridUnitType.Star);

            //    gridButtons.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //    gridButtons.Children.Remove(btnNewClient);
            //    gridButtons.Children.Remove(btnOrder);
            //    gridButtons.Children.Remove(btnSync);
            //    //gridButtons.Children.Remove(btnBilled);

            //    gridButtons.Children.Add(btnNewClient, 0, 0);
            //    gridButtons.Children.Add(btnOrder, 1, 0);
            //    gridButtons.Children.Add(btnSync, 2, 0);
            //    //gridButtons.Children.Add(btnBilled, 3, 0);
            //}
            //else //Retrato
            //{
            //    rowPlantas.Height = new GridLength(1, GridUnitType.Auto);
            //    //rowPlantas.Height = new GridLength(.1, GridUnitType.Star);
            //    rowCarousel.Height = new GridLength(.3, GridUnitType.Star);
            //    rowButtons.Height = new GridLength(.6, GridUnitType.Star);

            //    gridButtons.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //    gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //    gridButtons.Children.Remove(btnNewClient);
            //    gridButtons.Children.Remove(btnOrder);
            //    gridButtons.Children.Remove(btnSync);
            //    //gridButtons.Children.Remove(btnBilled);

            //    gridButtons.Children.Add(btnNewClient, 0, 0);
            //    gridButtons.Children.Add(btnOrder, 1, 0);
            //    gridButtons.Children.Add(btnSync, 0, 1);
            //    //gridButtons.Children.Add(btnBilled, 1, 1);
            //}
        }


        //protected override async void OnAppearing()
        //{
        //    var content = Content;
        //    Content = null;
        //    Content = content;

        //    //_animate = true;
        //    //await AnimateIn();
        //}

        //protected override void OnDisappearing()
        //{
        //    _animate = false;
        //}

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