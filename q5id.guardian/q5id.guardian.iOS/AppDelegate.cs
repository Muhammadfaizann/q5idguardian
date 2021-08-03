using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;
using Xamarin.Forms.GoogleMaps.iOS;

namespace q5id.guardian.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();
            //Xamarin.FormsMaps.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyANi77wVc7W33Z4UW_9_2dUCobRbQiB16E");
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
