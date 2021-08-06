using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace q5id.guardian.Droid
{
    [Activity(Label = "Guardian", Icon = "@mipmap/icon", Theme = "@style/MainTheme.Splash",
        MainLauncher = true,
        NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, MainApp, App>
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override async Task RunAppStartAsync(Bundle bundle)
        {
            
            await base.RunAppStartAsync(bundle);

            StartActivity(typeof(MainActivity));
        }

        //protected override void RunAppStart(Bundle bundle)
        //{
        //    StartActivity(typeof(MainActivity));
        //    base.RunAppStart(bundle);
        //}
    }
}