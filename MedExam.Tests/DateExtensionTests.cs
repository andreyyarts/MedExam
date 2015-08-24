using System;
using FluentAssertions;
using MedExam.Common.Extensions;
using NUnit.Framework;

namespace MedExam.Tests
{
    [TestFixture]
    public class DateExtensionTests
    {
        [Test, Sequential]
        public void Date_GetYearsBefore_should_return_correct_yaers([Values(3, 3, 3)]int month, [Values(2, 3, 4)]int day, [Values(26, 26, 25)]int result)
        {
            var dateBefore = new DateTime(1989, month, day);
            var currentDate = new DateTime(2015, 3, 3);

            dateBefore.GetYearsBefore(currentDate).Should().Be(result);
        }

        [Test, Sequential]
        public void Date_GetYearsBefore_should_return_correct_yaers([Values(1989, 1990, 1991, 1992, 1993)]int year, [Values(26, 25, 24, 23, 22)]int result)
        {
            var dateBefore = new DateTime(year, 3, 3);
            var currentDate = new DateTime(2015, 3, 3);

            dateBefore.GetYearsBefore(currentDate).Should().Be(result);
        }
    }
}
