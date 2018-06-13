using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;
using Foundation;
using HockeyApp.iOS;
using UIKit;

namespace KraftSales.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        const string HOCKEYAPP_APPID = "a19207e918ee42e1b671076b9afa9e60";
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(HOCKEYAPP_APPID);
            manager.DisableUpdateManager = false;
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();


            Xfx.XfxControls.Init();
            Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
