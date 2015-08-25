using System;
using FluentAssertions;
using MedExam.Common.Extensions;
using NUnit.Framework;

namespace MedExam.Tests
{
    [TestFixture]
    public class DateExtensionTests
    {
        [Test]
        public void Date_GetYearsBefore_should_return_correct_yaers([Values(1989, 1990, 1991, 1992, 1993)]int year, [Values(1, 2, 3, 4, 5)]int month, [Values(1, 2, 3, 4, 5)]int day, [Values(2011, 2012, 2013, 2014, 2015)]int currentYear)
        {
            var dateBefore = new DateTime(year, month, day);
            var currentDate = new DateTime(currentYear, 3, 3);

            var expectedYears = currentDate.Year - year
                                - (currentDate.Month > month
                                   || currentDate.Month == month && currentDate.Day >= day
                                    ? 0
                                    : 1);

            dateBefore.GetYearsBefore(currentDate).Should().Be(expectedYears);
        }
    }
}
