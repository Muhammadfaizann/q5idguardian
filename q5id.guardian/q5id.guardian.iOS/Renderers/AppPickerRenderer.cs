using System;
using System.ComponentModel;
using CoreGraphics;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppPicker), typeof(AppPickerRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppPickerRenderer : PickerRenderer
    {
        private AppPicker mAppPicker;
        private UITextFieldPadding mEditText;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            if (mAppPicker == null)
            {
                mAppPicker = (AppPicker)Element;
                UpdateEntryBackground();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppPicker.CornerRadiusProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppPicker.BorderThicknessProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppPicker.NormalBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppPicker.FocusBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }

            base.OnElementPropertyChanged(sender, e);
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
            if (mEditText != null)
            {
                mEditText.EditingDidBegin -= Control_EditingDidBegin;
                mEditText.EditingDidEnd -= Control_EditingDidEnd;
            }
        }

        protected void UpdateBackground(UITextField control)
        {
            if (control == null || mAppPicker == null) return;
            control.Layer.CornerRadius = mAppPicker.CornerRadius;
            control.Layer.BorderWidth = mAppPicker.BorderThickness;
            CGColor borderColor = control.IsEditing ? mAppPicker.FocusBorderColor.ToCGColor() : mAppPicker.NoramlBorderColor.ToCGColor();
            control.Layer.BorderColor = borderColor;
        }

        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
