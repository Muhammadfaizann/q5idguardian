using System;
using System.Diagnostics;
using Android.Content;
using Android.Util;
using Android.Widget;
using Plugin.CurrentActivity;
using q5id.guardian.DependencyServices;
using q5id.guardian.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppDeviceService))]
namespace q5id.guardian.Droid.DependencyServices
{
    public class AppDeviceService : IAppDeviceService
    {
        private static string SUBSCRIPTION_MANAGE_URL = "https://play.google.com/store/account/subscriptions?sku=guardian_subscriber&package=";

        public void DeviceLog(string message, object data)
        {
            Toast.MakeText(CrossCurrentActivity.Current.Activity, message, ToastLength.Short);
            Log.Debug("DeviceLog", message + " data: " + data.ToString());
        }

        public string GetDeviceId()
        {
            return Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }

        public void OpenSubscriptionManager()
        {
            try
            {
                Intent intent = new Intent(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse(GetSubscriptionProductManager()));
                intent.SetFlags(ActivityFlags.ClearTop);
                CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }
            catch (Exception e) {
                Debug.WriteLine("Open subscription manager error: ", e);
            }
        }

        private string GetSubscriptionProductManager()
        {
            return SUBSCRIPTION_MANAGE_URL + CrossCurrentActivity.Current.Activity.PackageName;
        }
    }
}
