using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Views;
using KraftSales.Helpers;
using System.Text;
using Xamarin.Forms.Platform.Android;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
//using Plugin.Permissions;

namespace KraftSales.Droid
{
    [Activity(
        Label = "DKP Fair", 
        Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private NfcAdapter _nfcAdapter;
        NfcHelper _nfcHelper;

        const string HOCKEYAPP_APPID = "eef8c4a39a954ab2a22067e11a33e265";

        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabs;

            base.OnCreate(bundle);

            CrashManager.Register(this, HOCKEYAPP_APPID);
            UpdateManager.Register(this, HOCKEYAPP_APPID);
            MetricsManager.Register(Application, HOCKEYAPP_APPID);

            SupportActionBar.SetDisplayShowHomeEnabled(true); // Show or hide the default home button
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowCustomEnabled(true); // Enable overriding the default toolbar layout
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            Xfx.XfxControls.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            UserDialogs.Init(this);


            _nfcHelper = new NfcHelper();

            // Get a reference to the default NFC adapter for this device. This adapter 
            // is how an Android application will interact with the actual hardware.
            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);

            LoadApplication(new App());

            Window window = this.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                window.SetStatusBarColor(Android.Graphics.Color.Rgb(68, 138, 255)); 
            }
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}


        protected override void OnNewIntent(Intent intent)
        {
            if (intent.GetParcelableExtra(NfcAdapter.ExtraTag) is Android.Nfc.Tag tag)
            {
                try
                {
                    // Get the string that was written to the NFC tag, and display it.
                    var ndef = Ndef.Get(tag);
                    if (ndef != null)
                    {
                        ndef.Connect();
                        var msg = ndef.NdefMessage;
                        var record = msg.GetRecords()[0];
                        var payload = Encoding.ASCII.GetString(record.GetPayload());
                        _nfcHelper.AdicionarMensagemLida(payload);
                    }
                }
                catch (System.Exception ex)
                {
                    //Toast.MakeText(this.ApplicationContext, ex.ToString(), ToastLength.Long).Show();
                }

                return;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            // App is paused, so no need to keep an eye out for NFC tags.
            if (_nfcAdapter != null)
                _nfcAdapter.DisableForegroundDispatch(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            // Create an intent filter for when an NFC tag is discovered.  When
            // the NFC tag is discovered, Android will u
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            // When an NFC tag is detected, Android will use the PendingIntent to come back to this activity.
            // The OnNewIntent method will invoked by Android.
            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (_nfcAdapter != null)
            {
                _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
            }
        }
    }
}

