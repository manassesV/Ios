using Android.Views.InputMethods;
using KraftSales.Services.DismissKeyboard;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.Droid.Services.DismissKeyboard.DismissKeyboardService))]
namespace KraftSales.Droid.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard()
        {
            InputMethodManager inputMethodManager = InputMethodManager.FromContext(CrossCurrentActivity.Current.Activity.ApplicationContext);

            inputMethodManager.HideSoftInputFromWindow(CrossCurrentActivity.Current.Activity.Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}