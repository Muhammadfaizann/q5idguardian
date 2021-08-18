using System;
using System.ComponentModel;
using CoreGraphics;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppDatePicker), typeof(AppDatePickerRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppDatePickerRenderer : DatePickerRenderer
    {
        private AppDatePicker mAppDatePicker;
        private UITextFieldPadding mEditText;

        public AppDatePickerRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            if (mAppDatePicker == null)
            {
                mAppDatePicker = (AppDatePicker)Element;
                if (mAppDatePicker.NullableDate.HasValue == false)
                {
                    mEditText.Text = mAppDatePicker.Placeholder;
                }
                UpdateEntryBackground();
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppDatePicker.CornerRadiusProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppDatePicker.BorderThicknessProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppDatePicker.NormalBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppDatePicker.FocusBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }

            if(e.PropertyName == AppDatePicker.DateProperty.PropertyName || e.PropertyName == AppDatePicker.NullableDateProperty.PropertyName){
                if (mAppDatePicker.NullableDate.HasValue == false)
                {
                    mEditText.Text = mAppDatePicker.Placeholder;
                }
                else
                {
                    mEditText.Text = mEditText.Text;
                }
            }
            base.OnElementPropertyChanged(sender, e);
            
        }


        protected void UpdateBackground(UITextField control)
        {
            if (control == null || mAppDatePicker == null) return;
            control.Layer.CornerRadius = mAppDatePicker.CornerRadius;
            control.Layer.BorderWidth = mAppDatePicker.BorderThickness;
            CGColor borderColor = control.IsEditing ? mAppDatePicker.FocusBorderColor.ToCGColor() : mAppDatePicker.NoramlBorderColor.ToCGColor();
            control.Layer.BorderColor = borderColor;
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

        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
