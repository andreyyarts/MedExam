using System;
using System.Globalization;
using System.Windows.Data;

namespace MedExam.Common.Converters
{
    public class DateTimeToMonthGenitiveNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            var month = 1;
            if (value is DateTime)
            {
                month = ((DateTime)value).Month;
            }

            var names = culture.DateTimeFormat.MonthGenitiveNames;

            return names[month - 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}