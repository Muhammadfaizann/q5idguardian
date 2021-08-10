using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppPicker), typeof(AppPickerRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppPickerRenderer : PickerRenderer
    {
        private AppPicker mAppPicker;
        private Android.Widget.EditText mEditText;

        public AppPickerRenderer(Context context) : base(context)
        {
        }

        private void OnEntryFocusChange(object sender, FocusChangeEventArgs e)
        {
            UpdateEntryBackground();
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Element == null || Control == null)
            {
                return;
            }
            if (mAppPicker == null)
            {
                mAppPicker = (AppPicker)Element;
            }
            if (mEditText == null)
            {
                mEditText = Control;
                mEditText.Focusable = true;
                mEditText.FocusableInTouchMode = true;
                mEditText.FocusChange += OnEntryFocusChange;
                UpdateBackground(mEditText);
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

        protected override void OnFocusChangeRequested(object sender, VisualElement.FocusRequestArgs e)
        {
            base.OnFocusChangeRequested(sender, e);
            UpdateEntryBackground();
        }

        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, Android.Graphics.Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
            UpdateEntryBackground();
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateEntryBackground();
        }
        protected void UpdateBackground(Android.Widget.EditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(mAppPicker.CornerRadius));
            Android.Graphics.Color borderColor = control.IsFocused ? mAppPicker.FocusBorderColor.ToAndroid() : mAppPicker.NoramlBorderColor.ToAndroid();
            gd.SetStroke((int)Context.ToPixels(mAppPicker.BorderThickness), borderColor);
            control.SetBackground(gd);


            var padTop = (int)Context.ToPixels(mAppPicker.Padding.Top);
            var padBottom = (int)Context.ToPixels(mAppPicker.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(mAppPicker.Padding.Left);
            var padRight = (int)Context.ToPixels(mAppPicker.Padding.Right);

            control.SetPadding(padLeft, 0, padRight, 0);
        }


        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
