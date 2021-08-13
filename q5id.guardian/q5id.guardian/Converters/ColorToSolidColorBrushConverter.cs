using System;
using System.Globalization;
using Xamarin.Forms;

namespace q5id.guardian.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Color color)
            {
                return color == null ? new SolidColorBrush(color) : null;
            }
            if(value is string st)
            {
                return !String.IsNullOrEmpty(st) ? new SolidColorBrush(Color.FromHex(st)) : null;
            }
            return null;
        }
            

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is SolidColorBrush brush)
            {
                return brush?.Color;
            }
            return null;
        }
    }
}
