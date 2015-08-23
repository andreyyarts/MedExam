using System.Linq;
using System.Windows.Documents;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.Reports.Views;
using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class BloodTestRpgaForTyphoidReport : IReportFlow
    {
        private readonly PatientReportService _patientReportService;
        private readonly long[] _patientIds;

        public BloodTestRpgaForTyphoidReport(PatientReportService patientReportService, long[] patientIds)
        {
            _patientReportService = patientReportService;
            _patientIds = patientIds;
        }

        public string Title
        {
            get { return "Анализ крови: РПГА на брюшной тиф"; }
        }

        public int CountInWidth
        {
            get { return 2; }
        }

        public int CountInHeight
        {
            get { return 2; }
        }

        public Block Report
        {
            get
            {
                var view = new BloodTestRpgaForTyphoidReportView();
                return view.ReportBlock;
            }
        }

        public object[] Datas
        {
            get
            {
                var localSettings = new
                {
                    OrganizationName = string.Concat("ГБУЗ ТО ", SpecialChars.Laquo, "ОКБ №1", SpecialChars.Raquo),
                    DepartmentName = "ОТДЕЛЕНИЕ ПРОФОСМОТРОВ",
                    MedExamDoctorName = "Куликова Л.В."
                };

                var patients = _patientReportService.LoadPatientsByIds(_patientIds);

                var datas = patients.Select(patient => new DirectionInImmunologyLaboratoryReportViewModel
                {
                    CurrentOrganizationName = localSettings.OrganizationName,
                    CurrentDepartmentName = localSettings.DepartmentName,
                    DoctorNameWithInitials = localSettings.MedExamDoctorName,
                    PatientFullName = patient.PersonName.FullName,
                    PatientAge = patient.BirthDate.Value.ToShortDateString(),
                    PatientOrganizationName = patient.OrganizationName
                }).Cast<object>().ToArray();

                return datas;
            }
        }
    }
}