using System;
using Android.Content;
using Android.Util;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AppMediaElement), typeof(AppMediaElementRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppMediaElementRenderer : MediaElementRenderer
    {
        public AppMediaElementRenderer(Context context) : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            catch(Exception ex)
            {
                Log.Error("Guardian", "Cannot dispose object: " + ex.Message);
            }
        }
    }
}
