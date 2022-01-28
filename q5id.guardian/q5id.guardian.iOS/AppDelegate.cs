using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Platform;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Core;
using Plugin.Geolocator;
using StoreKit;
using UIKit;

namespace q5id.guardian.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxFormsApplicationDelegate<Setup, MainApp, App>
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public static NSData PushDeviceToken { get; private set; } = null;
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            Xamarin.FormsMaps.Init();
            CarouselViewRenderer.Init();
            Xamarin.Forms.Forms.Init();
            //Xamarin.FormsGoogleMaps.Init("AIzaSyANi77wVc7W33Z4UW_9_2dUCobRbQiB16E");
            Plugin.InAppBilling.InAppBillingImplementation.OnShouldAddStorePayment = OnShouldAddStorePayment;


            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());
                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }


            return base.FinishedLaunching(application, launchOptions);
        }

        private bool OnShouldAddStorePayment(SKPaymentQueue arg1, SKPayment arg2, SKProduct arg3)
        {
            return true;
        }

        /// <summary>
        /// Called when the push notification system is registered
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="deviceToken">Device token.</param>
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            byte[] result = new byte[deviceToken.Length];
            Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);
            PushDeviceToken = BitConverter.ToString(result).Replace("-", "");
            System.Diagnostics.Debug.WriteLine($"TOKEN REC: {PushDeviceToken}");
            Utils.Utils.SavePushNotificationToken(BitConverter.ToString(result).Replace("-", ""));
        }

        public override void DidReceiveRemoteNotification(UIApplication application,
            NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

            NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

            ProcessNotification(aps);
        }

        void ProcessNotification(NSDictionary userInfo)
        {
            if (userInfo == null)
                return;

            var apsKey = new NSString("alert");
            var titleKey = new NSString("title");
            var bodyKey = new NSString("body");

            if (userInfo.ContainsKey(apsKey))
            {
                var aps = (NSDictionary)userInfo.ObjectForKey(apsKey);

                var title = (NSString)aps.ObjectForKey(titleKey);
                var body = (NSString)aps.ObjectForKey(bodyKey);

                string titleContent = string.Empty, bodyContent = string.Empty;

                if (!string.IsNullOrEmpty(title))
                {
                    titleContent = title.ToString();
                }

                if (!string.IsNullOrEmpty(body))
                {
                    bodyContent = body.ToString();
                }

                UIAlertView avAlert = new UIAlertView(titleContent, bodyContent, null, "OK", null);
                avAlert.Show();

            }

        }
    }
}
