using System;
using System.Collections.Generic;
using MedExam.Patient.Reports;

namespace MedExam
{
    public static class ReportTypes
    {
        public static IEnumerable<Type> GetAll()
        {
            return new[]
            {
                typeof (DirectionReport)
            };
        }
    }
}