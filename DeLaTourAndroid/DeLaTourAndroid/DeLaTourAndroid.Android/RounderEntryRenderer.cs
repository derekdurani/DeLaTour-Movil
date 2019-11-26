using DeLaTourAndroid;
using DeLaTourAndroid.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry),
typeof(RounderEntryRenderer))]
namespace DeLaTourAndroid.Droid
{
    class RounderEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Forms.Context.GetDrawable(Resource.Drawable.RoundedEditText);
            }
        }
    }
}