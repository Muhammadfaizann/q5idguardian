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

[assembly: ExportRenderer(typeof(AppEntry), typeof(AppEntryRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppEntryRenderer : EntryRenderer
    {
        private AppEntry mAppEntry;
        private Android.Widget.EditText mEditText;

        public AppEntryRenderer(Context context) : base(context)
        {
        }

        private void OnEntryFocusChange(object sender, FocusChangeEventArgs e)
        {
            UpdateEntryBackground();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Element == null || EditText == null)
            {
                return;
            }
            if(mAppEntry == null)
            {
                mAppEntry = (AppEntry)Element;
            }
            if(mEditText == null)
            {
                mEditText = EditText;
                mEditText.Focusable = true;
                mEditText.FocusableInTouchMode = true;
                mEditText.FocusChange += OnEntryFocusChange;
                UpdateBackground(mEditText);
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
            gd.SetCornerRadius(Context.ToPixels(mAppEntry.CornerRadius));
            Android.Graphics.Color borderColor = control.IsFocused ? mAppEntry.FocusBorderColor.ToAndroid() : mAppEntry.NoramlBorderColor.ToAndroid();
            gd.SetStroke((int)Context.ToPixels(mAppEntry.BorderThickness), borderColor);
            control.SetBackground(gd);


            var padTop = (int)Context.ToPixels(mAppEntry.Padding.Top);
            var padBottom = (int)Context.ToPixels(mAppEntry.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(mAppEntry.Padding.Left);
            var padRight = (int)Context.ToPixels(mAppEntry.Padding.Right);

            control.SetPadding(padLeft, 0, padRight, 0);
        }


        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
