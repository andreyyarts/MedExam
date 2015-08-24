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
        private readonly LocalSettings _localSettings;
        private readonly PatientReportService _patientReportService;
        private object[] _datas;
        private readonly long[] _patientIds;

        public BloodTestRpgaForTyphoidReport(LocalSettings localSettings, PatientReportService patientReportService, long[] patientIds)
        {
            _localSettings = localSettings;
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
                if (_datas != null)
                    return _datas;

                var patients = _patientReportService.LoadPatientsByIds(_patientIds);

                _datas = patients.Select(patient => new DirectionInImmunologyLaboratoryReportViewModel
                {
                    CurrentOrganizationName = _localSettings.OrganizationName,
                    CurrentDepartmentName = _localSettings.DepartmentName,
                    DoctorNameWithInitials = _localSettings.MedExamDoctorName,
                    PatientFullName = patient.PersonName.FullName,
                    PatientAge = patient.BirthDate.Value.ToShortDateString(),
                    PatientOrganizationName = patient.OrganizationName
                }).Cast<object>().ToArray();

                return _datas;
            }
        }
    }
}