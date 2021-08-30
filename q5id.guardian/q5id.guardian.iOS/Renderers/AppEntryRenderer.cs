using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using q5id.guardian.iOS.Controls;

[assembly: ExportRenderer(typeof(AppEntry), typeof(AppEntryRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppEntryRenderer : EntryRenderer
    {
        private AppEntry mAppEntry;
        private UITextFieldPadding mEditText;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            if (mAppEntry == null)
            {
                mAppEntry = (AppEntry)Element;
                UpdateEntryBackground();
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEntry.NormalBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEntry.FocusBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        
        protected void UpdateBackground(UITextField control)
        {
            if (control == null || mAppEntry == null) return;
            control.Layer.CornerRadius = mAppEntry.CornerRadius;
            control.Layer.BorderWidth = mAppEntry.BorderThickness;
            CGColor borderColor = control.IsEditing ? mAppEntry.FocusBorderColor.ToCGColor() : mAppEntry.NoramlBorderColor.ToCGColor();
            control.Layer.BorderColor = borderColor;
            control.TextColor = mAppEntry.TextColor.ToUIColor();
        }

        protected override UITextField CreateNativeControl()
        {
            mEditText = new UITextFieldPadding(CGRect.Empty);
            mEditText.EditingDidBegin += Control_EditingDidBegin;
            mEditText.EditingDidEnd += Control_EditingDidEnd;
            UpdateBackground(mEditText);

            return mEditText;
        }

        private void Control_EditingDidEnd(object sender, EventArgs e)
        {
            UpdateEntryBackground();
        }

        private void Control_EditingDidBegin(object sender, EventArgs e)
        {
            UpdateEntryBackground();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(mEditText != null)
            {
                mEditText.EditingDidBegin -= Control_EditingDidBegin;
                mEditText.EditingDidEnd -= Control_EditingDidEnd;
            }
        }

        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
