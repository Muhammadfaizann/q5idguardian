using System;
using q5id.guardian.DependencyServices;
using q5id.guardian.iOS.DependencyServices;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppDeviceService))]
namespace q5id.guardian.iOS.DependencyServices
{
    public class AppDeviceService : IAppDeviceService
    {
        private static string SUBSCRIPTION_MANAGE_URL = "itmss://buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/manageSubscriptions";

        public void DeviceLog(string message, object data)
        {
            Console.WriteLine(message + " data: " + data.ToString());
        }

        public string GetDeviceId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }

        public void OpenSubscriptionManager()
        {
            UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(SUBSCRIPTION_MANAGE_URL));
        }
    }
}
