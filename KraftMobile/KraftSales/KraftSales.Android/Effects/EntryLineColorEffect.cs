using Xamarin.Forms;
using KraftSales.Droid.Effects;
using Xamarin.Forms.Platform.Android;
using System;
using Android.Widget;
using KraftSales.Behaviors;
using System.ComponentModel;
using System.Diagnostics;

[assembly: ResolutionGroupName("KraftSales")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace KraftSales.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as EditText;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}