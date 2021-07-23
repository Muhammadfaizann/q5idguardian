
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace q5id.guardian.Droid
{
    [Activity(Label = "Guardian", Icon = "@mipmap/icon", Theme = "@style/MainTheme.Splash", MainLauncher = true)]
    public class SplashActivity : Activity
    {
        private Boolean isStartUp = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (!isStartUp)
            {
                isStartUp = true;
                Startup();
            }
        }

        private void Startup()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
            this.FinishAffinity();
        }
    }
}
