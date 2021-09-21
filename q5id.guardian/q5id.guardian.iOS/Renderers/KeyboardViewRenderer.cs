using System;
using CoreGraphics;
using Foundation;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardView), typeof(KeyboardViewRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class KeyboardViewRenderer : ViewRenderer
    {
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        private KeyboardView mKeyboardView;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            }

            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }
            mKeyboardView = (KeyboardView)Element;
        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {

            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            if (Element != null)
            {
                var elementMargin = Element.Margin;
                elementMargin.Bottom = keyboardSize.Height;
                Element.Margin = elementMargin; //push the entry up to keyboard height when keyboard is activated
            }
            mKeyboardView.UpdateKeyBoardState(true);
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            if (Element != null)
            {
                var elementMargin = Element.Margin;
                elementMargin.Bottom = 0;
                Element.Margin = elementMargin; //set the margins to zero when keyboard is dismissed
            }
            mKeyboardView.UpdateKeyBoardState(false);
        }


        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
    }
}
