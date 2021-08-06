using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using q5id.guardian.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace q5id.guardian.Droid.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        public CustomNavigationRenderer(Context context) : base(context)
        {
           
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            return base.OnPushAsync(view, false);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            return base.OnPopViewAsync(page, false);
        }
    }
}