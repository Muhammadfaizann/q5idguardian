using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppEditor), typeof(AppEditorRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppEditorRenderer : EditorRenderer
    {
        private AppEditor mAppEditor;
        private AppTextView mTextView;
        private string mPlaceHolder;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            if (mAppEditor == null)
            {
                mAppEditor = (AppEditor)Element;
                UpdateEntryBackground();
            }
            if (Control != null)
            {
                //Control.Delegate = null;
                //Control.Delegate = new AppTextViewDelegate(mTextView);
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

        protected void UpdateBackground(AppTextView control)
        {
            if (control == null || mAppEditor == null) return;
            control.Layer.CornerRadius = mAppEditor.CornerRadius;
            control.Layer.BorderWidth = mAppEditor.BorderThickness;
            CGColor borderColor = control.IsEditing ? mAppEditor.FocusBorderColor.ToCGColor() : mAppEditor.NoramlBorderColor.ToCGColor();
            control.Layer.BorderColor = borderColor;
        }

        protected override UITextView CreateNativeControl()
        {
            mTextView = new AppTextView(CGRect.Empty);
            mTextView.Started += Control_EditingDidBegin;
            mTextView.Ended += Control_EditingDidEnd;
            UpdateBackground(mTextView);

            return mTextView;
        }

        private void Control_EditingDidEnd(object sender, EventArgs e)
        {
            mTextView.IsEditing = false;
            UpdateEntryBackground();
        }

        private void Control_EditingDidBegin(object sender, EventArgs e)
        {
            mTextView.IsEditing = true;
            UpdateEntryBackground();
        }

        protected override void Dispose(bool disposing)
        {
            if (mTextView != null)
            {
                mTextView.Delegate = null;
                mTextView.Started -= Control_EditingDidBegin;
                mTextView.Ended -= Control_EditingDidEnd;
            }
            base.Dispose(disposing);
            
        }


        private void UpdateEntryBackground()
        {
            UpdateBackground(mTextView);
        }
    }

    public class AppTextView : UITextView
    {
        public bool IsEditing = false;

        public AppTextView(CGRect rect) : base(rect)
        {

        }
    }
}
