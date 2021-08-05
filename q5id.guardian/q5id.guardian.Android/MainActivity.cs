using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using FFImageLoading.Forms.Platform;
using System.Collections.Generic;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using Plugin.CurrentActivity;
using MvvmCross.Forms.Platforms.Android.Views;

namespace q5id.guardian.Droid
{
    [Activity(Label = "Guardian", Icon = "@mipmap/icon", Theme = "@style/MainTheme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, MainApp, App>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 229, 59, 85));
            
        }

        protected override void OnStart()
        {
            base.OnStart();

           // RequestPermissions();
        }

        private void RequestPermissions()
        {
            PackageInfo info = PackageManager.GetPackageInfo(this.PackageName, PackageInfoFlags.Permissions);
            var listPermissionDefined = info.RequestedPermissions;
            List<string> listPermissionNotGrant = new List<string>();
            
            foreach (String permission in listPermissionDefined)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
                {
                    listPermissionNotGrant.Add(permission);
                }
            }
            if(listPermissionNotGrant.Count > 0)
            {
                string[] permissions = new string[listPermissionNotGrant.Count];
                listPermissionNotGrant.CopyTo(permissions, 0);
                ActivityCompat.RequestPermissions(this, permissions, 65);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            foreach (Android.Content.PM.Permission grantResult in grantResults)
            {
                if(grantResult == Permission.Denied)
                {
                    RequestPermissions();
                    return;
                }
            }
        }
    }
}