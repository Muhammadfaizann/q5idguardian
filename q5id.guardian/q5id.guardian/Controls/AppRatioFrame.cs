using System;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppRatioFrame : Frame
    {
        public AppRatioFrameBaseOptions RatioBase { get; set; }

        public float RatioValue { get; set; }

        public AppRatioFrame()
        {
            HasShadow = false;
            Padding = 0;
            BorderColor = Color.Transparent;
            CornerRadius = 0;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if(width > 0 || height > 0)
            {
                if(RatioValue > 0)
                {
                    double resultWidth = width;
                    double resultHeight = height;
                    if (RatioBase == AppRatioFrameBaseOptions.Width)
                    {
                        resultHeight = resultWidth * RatioValue;
                        this.HeightRequest = resultHeight;
                    }
                    else
                    {
                        resultWidth = resultHeight * RatioValue;
                        this.WidthRequest = resultWidth;
                    }
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            //if (RatioValue <= 0)
            //{
            //    return base.OnMeasure(widthConstraint, heightConstraint);
            //}
            
            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }

    public enum AppRatioFrameBaseOptions
    {
        Width,
        Height
    }
}
