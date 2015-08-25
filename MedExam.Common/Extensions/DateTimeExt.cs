using System;

namespace MedExam.Common.Extensions
{
    public static class DateTimeExt
    {
        public static int GetYearsBefore(this DateTime date, DateTime today)
        {
            var diffDate = today.AddYears(-date.Year).AddMonths(-date.Month + 1).AddDays(-date.Day + 1);
            return diffDate.Year;
        }
    }
}