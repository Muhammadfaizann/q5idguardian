using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using AndroidX.CardView.Widget;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(AppFrame), typeof(AppFrameRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppFrameRenderer : FrameRenderer
    {

        CardView mCardView = null;
        AppFrame mAppFrame = null;
        Boolean mIsTouching = false;

        public AppFrameRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null && mCardView == null && mAppFrame == null)
            {
                mCardView = Control;
                mCardView.Touch += ThisButton_Touch;
                if(Element is AppFrame appFrame)
                {
                    mAppFrame = appFrame;
                }
            }
        }

        private void ThisButton_Touch(object sender, TouchEventArgs e)
        {
            Log.Debug("Mantheon", "ThisButton_Touch: " + e.Event.Action);
            if (e.Event.Action == MotionEventActions.Down)
            {
                mIsTouching = true;
                mCardView?.Invalidate();
            }
            else if (e.Event.Action == MotionEventActions.Cancel)
            {
                mIsTouching = false;
                mCardView?.Invalidate();
            }
            else if (e.Event.Action == MotionEventActions.Move)
            {
                float posX = e.Event.GetX(e.Event.ActionIndex);
                float posY = e.Event.GetY(e.Event.ActionIndex);
                float contentWidth = mCardView.Width;
                float contentHeight = mCardView.Height;
                if (posX < 0 || posY < 0 || posX > contentWidth || posY > contentHeight)
                {
                    //Move outside
                    mIsTouching = false;
                    mCardView?.Invalidate();
                }
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                if(mIsTouching == true)
                {
                    mAppFrame?.SendClickedCommand();
                }
                mIsTouching = false;
                mCardView?.Invalidate();
                
            }
        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            if (this.mAppFrame.IsDisable)
            {
                DrawDeactiveBackground(canvas);
            }
            else
            {
                if (this.mIsTouching)
                {
                    DrawPressedBackground(canvas);
                }
                else
                {
                    DrawNormalBackground(canvas);
                }
            }
            base.DispatchDraw(canvas);
        }

        private void DrawNormalBackground(global::Android.Graphics.Canvas canvas)
        {
            if(this.mAppFrame == null)
            {
                return;
            }
            #region for Horizontal Gradient
            var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,
            #endregion

                   this.mAppFrame.NormalBackgroundColorStart.ToAndroid(),
                   this.mAppFrame.NormalBackgroundColorEnd.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
        }

        private void DrawPressedBackground(global::Android.Graphics.Canvas canvas)
        {
            if (this.mAppFrame == null)
            {
                return;
            }
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = this.mAppFrame.PressedBackgroundColor.ToAndroid();
            canvas.DrawPaint(paint);
        }

        private void DrawDeactiveBackground(global::Android.Graphics.Canvas canvas)
        {
            if (this.mAppFrame == null)
            {
                return;
            }
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = this.mAppFrame.DeactiveBackgroundColor.ToAndroid();
            canvas.DrawPaint(paint);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(AppFrame.IsDisable))
            {
                this.mCardView?.Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (mCardView != null)
            {
                mCardView.Touch -= ThisButton_Touch;
            }
            base.Dispose(disposing);
        }
    }
}
