using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Plugin.CurrentActivity;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(KeyboardView), typeof(KeyboardViewRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class KeyboardViewRenderer : ViewRenderer
    {
        public Android.Views.View ActivityRootView { get; private set; }
        private int usableHeightPrevious;
        private KeyboardView mKeyboardView;

        public KeyboardViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }

            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            }
            mKeyboardView = (KeyboardView)Element;
        }

        void RegisterForKeyboardNotifications()
        {
            var currentAct = CrossCurrentActivity.Current.Activity;
            ActivityRootView = currentAct.Window.DecorView.FindViewById(Android.Resource.Id.Content);
            ActivityRootView.ViewTreeObserver.GlobalLayout += ViewTreeObserver_GlobalLayout;
        }

        private void ViewTreeObserver_GlobalLayout(object sender, EventArgs e)
        {
            PossiblyResizeChildOfContent();
        }

        private void PossiblyResizeChildOfContent()
        {
            try
            {
                Point screenSize = new Point();
                CrossCurrentActivity.Current.Activity.WindowManager?.DefaultDisplay?.GetSize(screenSize);
                var screenHeight = screenSize.Y;

                var rootView = CrossCurrentActivity.Current.Activity.Window.DecorView.FindViewById(Android.Resource.Id.Content);

                if (rootView == null)
                {
                    return;
                }

                Rect screenHeightWithoutKeyboard = new Rect();
                rootView.GetWindowVisibleDisplayFrame(screenHeightWithoutKeyboard);

                int keyboardHeight;

                // Android officially supports display cutouts on devices running Android 9 (API level 28) and higher.
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.P)
                {
                    var displayCutout = rootView.RootWindowInsets?.DisplayCutout;

                    // Displays with a cutout need to be handled different due to the cutout type:
                    //
                    // Default cutout:
                    // A display has no cutout. The screen height can be used used as usual.
                    //
                    // Corner cutout:
                    // A display has a cutout in a corner on the top of the display. The screen height must add the safe area of the top to get the total screen height.
                    //
                    // Double cutout:
                    // A display has a cutout in the middle on the top and in the middle on the bottom of the display.
                    // The screen height must add the safe area of the bottom only to get the total screen height.
                    // Adding the screen height of the top as well, would lead to false results.
                    //
                    // Tall cutout:
                    // A display has a cutout in the middle on the top of the display. The screen height must add the safe area of the top to get the total screen height.
                    keyboardHeight = displayCutout == null ?
                        screenHeight - screenHeightWithoutKeyboard.Bottom :
                        displayCutout.SafeInsetBottom <= 0 ?
                            screenHeight + displayCutout.SafeInsetTop - screenHeightWithoutKeyboard.Bottom :
                            screenHeight + displayCutout.SafeInsetBottom - screenHeightWithoutKeyboard.Bottom;
                }
                else
                {
                    keyboardHeight = screenHeight - screenHeightWithoutKeyboard.Bottom;
                }

                var keyboardHeightInDip = keyboardHeight / Resources?.DisplayMetrics.Density ?? 1;

                if (keyboardHeightInDip > 0.0f)
                {
                    OnKeyboardShow();
                }
                else
                {
                    OnKeyboardHide();
                }
            }
            catch(Exception ex)
            {
                Log.Debug("Error", "Cannot detect keyboard: " + ex.Message);
            }
        }

        void OnKeyboardShow()
        {
            mKeyboardView.UpdateKeyBoardState(true);
        }

        void OnKeyboardHide()
        {
            mKeyboardView.UpdateKeyBoardState(false);
        }


        void UnregisterForKeyboardNotifications()
        {
            if(ActivityRootView != null)
            {
                ActivityRootView.ViewTreeObserver.GlobalLayout -= ViewTreeObserver_GlobalLayout;
                ActivityRootView = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (ActivityRootView != null)
            {
                ActivityRootView.ViewTreeObserver.GlobalLayout -= ViewTreeObserver_GlobalLayout;
                ActivityRootView = null;
            }
            base.Dispose(disposing);
        }
    }
}
