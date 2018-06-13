using KraftSales.Services.DismissKeyboard;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(KraftSales.iOS.Services.DismissKeyboard.DismissKeyboardService))]
namespace KraftSales.iOS.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                vc.View.EndEditing(true);
            });
        }
    }
}