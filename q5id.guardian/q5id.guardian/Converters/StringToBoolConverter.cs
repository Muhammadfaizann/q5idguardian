using System;
using System.Globalization;
using Xamarin.Forms;

namespace q5id.guardian.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return false;
            }
            if(value is string strValue)
            {
                if(strValue.Trim() == "")
                {
                    return false;
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}

