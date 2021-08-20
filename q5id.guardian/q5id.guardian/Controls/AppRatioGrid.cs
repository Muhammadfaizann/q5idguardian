using System;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppRatioGrid : Grid
    {
        public AppRatioGridBaseOptions RatioBase { get; set; }

        public double RatioValue { get; set; }

        public AppRatioGrid()
        {
            SizeChanged += AppRatioGrid_SizeChanged;
        }

        private void AppRatioGrid_SizeChanged(object sender, EventArgs e)
        {
            double resultWidth = this.Width;
            double resultHeight = this.Height;
            if (resultWidth > 0 || resultHeight > 0)
            {
                if (RatioValue > 0)
                {

                    if (RatioBase == AppRatioGridBaseOptions.Width)
                    {
                        var newHeight = resultWidth * RatioValue;
                        if(newHeight != resultHeight)
                        {
                            this.HeightRequest = newHeight;
                            InvalidateMeasure();
                        }

                    }
                    else
                    {
                        var newWidth = resultHeight * RatioValue;
                        if (newWidth != resultWidth)
                        {
                            this.WidthRequest = newWidth;
                            InvalidateMeasure();
                        }
                        
                    }
                }
            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            double resultWidth = width;
            double resultHeight = height;
            
            base.OnSizeAllocated(resultWidth, resultHeight);
        }

    }

    public enum AppRatioGridBaseOptions
    {
        Width,
        Height
    }
}
