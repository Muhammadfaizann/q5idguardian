using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace q5id.guardian.Converters
{
    /// <summary>
    /// This class have methods to convert the string values to boolean.     
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ReverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the string to boolean.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value?.ToString(), out bool result))
            {
                return !result;
            }

            return value;
        }

        /// <summary>
        /// This method is used to convert the boolean to string.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value?.ToString(), out bool result))
            {
                return !result;
            }

            return value;
        }
    }
}
