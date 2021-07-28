using System;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppBoxView : BoxView
    {
        //Create a Bindable Property For CornerRadius  
        public static readonly BindableProperty BottomHeightProperty = BindableProperty.Create(nameof(BottomHeight), typeof(double), typeof(AppBoxView), 0.0);
        public double BottomHeight
        {
            get
            {
                return (double)GetValue(BottomHeightProperty);
            }
            set
            {
                SetValue(BottomHeightProperty, value);
            }
        }

        public AppBoxView()
        {
        }
    }
}
