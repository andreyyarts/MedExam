﻿using System;
using MedExam.Common.Interfaces;

namespace MedExam.Patient.Reports.ViewModels
{
    public class DirectionReportViewModel : IReportData
    {
        public string CurrentOrganizationName { get; set; }
        public string CurrentDepartmentName { get; set; }
        public string PatientFullName { get; set; }
        public string PatientOrganizationShortName { get; set; }
        public string PatientOrganizationFullName { get; set; }
        public string PatientAge { get; set; }
        public string DoctorNameWithInitials { get; set; }
        public DateTime CurrentDate { get; set; }
        public string HomeAddress { get; set; }
    }
}