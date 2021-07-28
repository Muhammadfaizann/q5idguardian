using System;
using CoreAnimation;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using UIKit;
using Xamarin.Forms;  
using Xamarin.Forms.Platform.iOS;  
[assembly: ExportRenderer(typeof(AppBoxView), typeof(AppBoxViewRenderer))]  
namespace q5id.guardian.iOS.Renderers
{
    public class AppBoxViewRenderer : BoxRenderer
    {
        private float _radiusHeight;
        private AppBoxView mAppBoxView;
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            if (Element == null || Element is AppBoxView == false) return;
            mAppBoxView = (AppBoxView)Element;
            Layer.MasksToBounds = true;
            _radiusHeight = (float)((AppBoxView)Element).BottomHeight;
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            if(mAppBoxView == null)
            {
                return;
            }
            var topRect = new CoreGraphics.CGRect(rect.Left, rect.Top, rect.Width, rect.Height - _radiusHeight);
            var topPath = UIBezierPath.FromRect(topRect);
            var backgroundColor = GetBackgroundColor();
            backgroundColor.SetFill();
            topPath.Fill();
            if(_radiusHeight > 0)
            {
                CoreGraphics.CGPoint pointA = new CoreGraphics.CGPoint(0, rect.Height - _radiusHeight);
                CoreGraphics.CGPoint pointB = new CoreGraphics.CGPoint(rect.Width, rect.Height - _radiusHeight);
                CoreGraphics.CGPoint pointC = new CoreGraphics.CGPoint(rect.Width/2, rect.Height);
                float circleRadius = (float)GetCircleRadius(pointA, pointB, pointC);
                
                var circleCenterPoint = new CoreGraphics.CGPoint(rect.Width / 2, rect.Height - circleRadius);
                var bottomPath = UIBezierPath.FromArc(circleCenterPoint, circleRadius, -180, 0, false);

                backgroundColor.SetFill();
                bottomPath.Fill();
            }
        }

        private UIColor GetBackgroundColor()
        {
            if (mAppBoxView?.BackgroundColor != null)
            {
                return mAppBoxView.BackgroundColor.ToUIColor();
            }
            else if (mAppBoxView?.Background != null && mAppBoxView?.Background is SolidColorBrush solidColorBrush)
            {
                return solidColorBrush.Color.ToUIColor();
            }
            else
            {
                return UIColor.Clear;
            }
        }

        private double GetCircleRadius(CoreGraphics.CGPoint pointA, CoreGraphics.CGPoint pointB, CoreGraphics.CGPoint pointC)
        {
            double a1 = 2 * pointB.X - 2 * pointA.X;
            double b1 = 2 * pointB.Y - 2 * pointA.Y;
            double c1 = pointA.X * pointA.X + pointA.Y * pointA.Y - pointB.X * pointB.X - pointB.Y * pointB.Y;

            double a2 = 2 * pointC.X - 2 * pointA.X;
            double b2 = 2 * pointC.Y - 2 * pointA.Y;
            double c2 = pointA.X * pointA.X + pointA.Y * pointA.Y - pointC.X * pointC.X - pointC.Y * pointC.Y;

            double yI = (-c2 - (a2 * -c1 / a1)) / (b2 - a2 * b1 / a1);
            double xI = -c1 / a1 - b1 * yI / a1;
            double doubleRadius = ((pointA.X - xI) * (pointA.X - xI)) + ((pointA.Y - yI) * (pointA.Y - yI));
            return Math.Sqrt(doubleRadius);
        }
    }
}
