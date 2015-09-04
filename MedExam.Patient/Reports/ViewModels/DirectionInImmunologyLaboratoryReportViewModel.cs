using System;

namespace MedExam.Patient.Reports.ViewModels
{
    public class DirectionInImmunologyLaboratoryReportViewModel
    {
        public string CurrentOrganizationName { get; set; }
        public string CurrentDepartmentName { get; set; }
        public string PatientFullName { get; set; }
        public string PatientOrganizationName { get; set; }
        public string PatientAge { get; set; }
        public string DoctorNameWithInitials { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}