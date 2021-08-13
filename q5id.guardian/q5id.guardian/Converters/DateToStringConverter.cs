using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace q5id.guardian.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        private const string DEFAULT_FORMAT = "MM/dd/yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is DateTime dateTime)
            {
                return dateTime.ToString(GetFormatFromParam(parameter));
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value != null && value is string dateTimeStr) { 
                    DateTime result = DateTime.ParseExact(dateTimeStr, GetFormatFromParam(parameter), null);

                    return result;
                }
                return null;
            }
            catch(Exception e)
            {
                Debug.WriteLine("DateToStringConverter: Can not convert back: " + e.Message);
                return null;
            }
           
            
        }

        private string GetFormatFromParam(object parameter)
        {
            if(parameter is string strParam)
            {
                return strParam;
            }
            return DEFAULT_FORMAT;
        }
    }
}
