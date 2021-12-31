using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace q5id.guardian.Droid
{
    [Application]
    public class GuardianApplication : Application
    {
        public GuardianApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            //Firebase.FirebaseApp.InitializeApp(this);


            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, new NotificationUserCategory[]
            {
            new NotificationUserCategory("message",new List<NotificationUserAction> {
                new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground),
                new NotificationUserAction("Forward","Forward",NotificationActionType.Foreground)

            }),
            new NotificationUserCategory("request",new List<NotificationUserAction> {
                new NotificationUserAction("Accept","Accept",NotificationActionType.Default,"check"),
                new NotificationUserAction("Reject","Reject",NotificationActionType.Default,"cancel")
            })

            }, true);
#else
	            FirebasePushNotificationManager.Initialize(this,new NotificationUserCategory[]
		    {
			new NotificationUserCategory("message",new List<NotificationUserAction> {
			    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground),
			    new NotificationUserAction("Forward","Forward",NotificationActionType.Foreground)

			}),
			new NotificationUserCategory("request",new List<NotificationUserAction> {
			    new NotificationUserAction("Accept","Accept",NotificationActionType.Default,"check"),
			    new NotificationUserAction("Reject","Reject",NotificationActionType.Default,"cancel")
			})

		    },false);
#endif

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("NOTIFICATION RECEIVED", p.Data);


            };

            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
                Utils.Utils.SavePushNotificationToken(p.Token);
            };
            UpdatePushNotificationToken();
        }

        private async void UpdatePushNotificationToken()
        {
            string token = await CrossFirebasePushNotification.Current.GetTokenAsync();
            System.Diagnostics.Debug.WriteLine($"TOKEN IS: {token}");
            Utils.Utils.SavePushNotificationToken(token);
        }
    }
}