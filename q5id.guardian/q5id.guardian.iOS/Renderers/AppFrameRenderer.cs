using System;
using System.ComponentModel;
using System.Diagnostics;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppFrame), typeof(AppFrameRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppFrameRenderer : FrameRenderer
    {
        UIKit.UIView mCardView;
        AppFrame mAppFrame;
        CALayer mBackgroundLayer;
        bool mIsTouching = false;
        UIKit.UIView mChildView;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            UpdateBackground();
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);
        }


        private void UpdateBackground()
        {
            if (this.mAppFrame != null)
            {
                if (this.mAppFrame.IsDisable)
                {
                    DrawDeactiveBackground();
                }
                else
                {
                    if (this.mIsTouching)
                    {
                        DrawPressedBackground();
                    }
                    else
                    {
                        DrawNormalBackground();
                    }
                }
            }
        }

        private void DrawNormalBackground()
        {
            if(this.mAppFrame == null)
            {
                return;
            }
            mBackgroundLayer?.RemoveFromSuperLayer();
            mBackgroundLayer = new CAGradientLayer();
            ((CAGradientLayer)mBackgroundLayer).Colors = new[] { this.mAppFrame.NormalBackgroundColorStart.ToCGColor(), this.mAppFrame.NormalBackgroundColorEnd.ToCGColor() };
            ((CAGradientLayer)mBackgroundLayer).StartPoint = new CGPoint(0, 0);
            ((CAGradientLayer)mBackgroundLayer).EndPoint = new CGPoint(1, 0);
            mBackgroundLayer.Frame = this.mCardView.Bounds;
            mBackgroundLayer.CornerRadius = this.mAppFrame.CornerRadius;
            this.mChildView.Layer.AddSublayer(mBackgroundLayer);
        }

        private void DrawPressedBackground()
        {
            if (this.mAppFrame == null)
            {
                return;
            }
            mBackgroundLayer?.RemoveFromSuperLayer();
            mBackgroundLayer = new CAShapeLayer();
            mBackgroundLayer.BackgroundColor = this.mAppFrame.PressedBackgroundColor.ToCGColor();
            mBackgroundLayer.Frame = this.mCardView.Bounds;
            mBackgroundLayer.CornerRadius = this.mAppFrame.CornerRadius;
            this.mChildView.Layer.AddSublayer(mBackgroundLayer);
            
        }

        private void DrawDeactiveBackground()
        {
            if (this.mAppFrame == null)
            {
                return;
            }
            mBackgroundLayer?.RemoveFromSuperLayer();
            mBackgroundLayer = new CAShapeLayer();
            mBackgroundLayer.BackgroundColor = this.mAppFrame.DeactiveBackgroundColor.ToCGColor();
            mBackgroundLayer.Frame = this.mCardView.Bounds;
            mBackgroundLayer.CornerRadius = this.mAppFrame.CornerRadius;
            this.mChildView.Layer.AddSublayer(mBackgroundLayer);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && this.GetControl() != null && mCardView == null && mAppFrame == null)
            {
                var control = this.GetControl();
                UIView childFrameView = null;
                if(control.Subviews.Length > 0)
                {
                    mCardView = control.Subviews[0];
                    if(mCardView.Subviews.Length > 0)
                    {
                        childFrameView = mCardView.Subviews[0];
                    }
                }
                mChildView = new UIView(mCardView.Bounds);
                mCardView.AddSubview(mChildView);
                if (childFrameView != null)
                {
                    mCardView.BringSubviewToFront(childFrameView);
                }

                if (Element is AppFrame appFrame)
                {
                    mAppFrame = appFrame;
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            if(this.mIsTouching == false)
            {
                this.mIsTouching = true;
                LayoutSubviews();
            }
            base.TouchesBegan(touches, evt);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            UITouch touch = touches.AnyObject as UITouch;
            CGPoint pointTouch = touch.LocationInView(this.mCardView);
            CGRect boundView = this.mCardView.Bounds;
            if(pointTouch.X < 0 || pointTouch.Y < 0 || pointTouch.X > boundView.Width || pointTouch.Y > boundView.Height)
            {
                if(this.mIsTouching == true)
                {
                    this.mIsTouching = false;
                    this.UpdateBackground();
                }
            }
            base.TouchesMoved(touches, evt);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            if (this.mIsTouching == true)
            {
                this.mIsTouching = false;
                this.UpdateBackground();
            }
            base.TouchesCancelled(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            if (this.mIsTouching == true)
            {
                this.mIsTouching = false;
                this.UpdateBackground();
                this.mAppFrame?.SendClickedCommand();
            }
            base.TouchesEnded(touches, evt);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}