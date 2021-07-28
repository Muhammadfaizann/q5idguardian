using System;
using Android.Content;
using Android.Graphics;  
using Android.Util;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms;  
using Xamarin.Forms.Platform.Android;  
[assembly: ExportRenderer(typeof(AppBoxView), typeof(AppBoxViewRenderer))]  
namespace q5id.guardian.Droid.Renderers
{
    public class AppBoxViewRenderer : BoxRenderer
    {
        private float _radiusHeight;
        private RectF _bounds;
        private Path _path;

        public AppBoxViewRenderer(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            var element = (AppBoxView)Element;
            
            _radiusHeight = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)element.BottomHeight, Context.Resources.DisplayMetrics);
        }
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if (w != oldw && h != oldh)
            {
                _bounds = new RectF(0, 0, w, h);
            }
            _path = new Path();
            _path.Reset();
            var topPartBounds = new RectF(0, 0, w, h - _radiusHeight);
            _path.AddRect(topPartBounds, Path.Direction.Cw);
            if(_radiusHeight > 0)
            {
                float circleRadius = (float)GetCircleRadius(0, h - _radiusHeight, w, h - _radiusHeight, w/2, h);
                _path.AddCircle(w / 2, h - circleRadius, circleRadius, Path.Direction.Cw);
            }
            _path.Close();
        }
        public override void Draw(Canvas canvas)
        {
            canvas.Save();
            canvas.ClipPath(_path);
            base.Draw(canvas);
            canvas.Restore();
        }

        private double GetCircleRadius(double xA, double yA, double xB, double yB, double xC, double yC)
        {
            double a1 = 2 * xB - 2 * xA;
            double b1 = 2 * yB - 2 * yA;
            double c1 = xA * xA + yA * yA - xB * xB - yB * yB;

            double a2 = 2 * xC - 2 * xA;
            double b2 = 2 * yC - 2 * yA;
            double c2 = xA * xA + yA * yA - xC * xC - yC * yC;

            double yI = (-c2 - (a2 * -c1 / a1)) / (b2 - a2 * b1 / a1);
            double xI = -c1 / a1 - b1 * yI / a1;
            double doubleRadius = ((xA - xI) * (xA - xI)) + ((yA - yI) * (yA - yI));
            return Math.Sqrt(doubleRadius);
        }
    }
}