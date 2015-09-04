using System;
using System.Globalization;
using System.Windows.Data;

namespace MedExam.Common.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "" : ((DateTime)value).ToString((string)parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}