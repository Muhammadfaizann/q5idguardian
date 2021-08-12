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

[assembly: ExportRenderer(typeof(AppEditor), typeof(AppEditorRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppEditorRenderer : EditorRenderer
    {
        private AppEditor mAppEditor;
        private Android.Widget.EditText mEditText;

        public AppEditorRenderer(Context context) : base(context)
        {
        }

        private void OnEntryFocusChange(object sender, FocusChangeEventArgs e)
        {
            UpdateEntryBackground();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Element == null || EditText == null)
            {
                return;
            }
            if (mAppEditor == null)
            {
                mAppEditor = (AppEditor)Element;
            }
            if (mEditText == null)
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
            if (e.PropertyName == AppEditor.CornerRadiusProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEditor.BorderThicknessProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEditor.NormalBorderColorProperty.PropertyName)
            {
                UpdateEntryBackground();
            }
            else if (e.PropertyName == AppEditor.FocusBorderColorProperty.PropertyName)
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
            gd.SetCornerRadius(Context.ToPixels(mAppEditor.CornerRadius));
            Android.Graphics.Color borderColor = control.IsFocused ? mAppEditor.FocusBorderColor.ToAndroid() : mAppEditor.NoramlBorderColor.ToAndroid();
            gd.SetStroke((int)Context.ToPixels(mAppEditor.BorderThickness), borderColor);
            control.SetBackground(gd);


            var padTop = (int)Context.ToPixels(mAppEditor.Padding.Top);
            var padBottom = (int)Context.ToPixels(mAppEditor.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(mAppEditor.Padding.Left);
            var padRight = (int)Context.ToPixels(mAppEditor.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }


        private void UpdateEntryBackground()
        {
            UpdateBackground(mEditText);
        }
    }
}
