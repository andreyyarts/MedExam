using System;

namespace MedExam.Common.Extensions
{
    public static class DateTimeExt
    {
        public static int GetYearsBefore(this DateTime date, DateTime today)
        {
            var zeroTime = new DateTime(1, 1, 1);
            var diff = today - date;
            // because we start at year 1 for the Gregorian calendar
            var years = (zeroTime + diff).Year - 1;
            return years;
        }
    }
}