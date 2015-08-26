using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using MedExam.Common;
using MedExam.Common.Extensions;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.Reports.Views;
using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class BloodTestRpgaForTyphoidReport : IReportFlow
    {
        private readonly LocalSettings _localSettings;
        private readonly SystemService _systemService;
        private readonly PatientReportService _patientReportService;
        private long[] _patientIds;

        public BloodTestRpgaForTyphoidReport(LocalSettings localSettings, SystemService systemService, PatientReportService patientReportService)
        {
            _localSettings = localSettings;
            _systemService = systemService;
            _patientReportService = patientReportService;
        }

        public void SetItems(long[] itemIds)
        {
            _patientIds = itemIds;
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

        public IEnumerable<object> Datas
        {
            get
            {
                if (_patientIds == null || _patientIds.Length == 0)
                    return new object[0];

                var today = _systemService.Today();
                var patients = _patientReportService.LoadPatientsByIds(_patientIds);

                var datas = patients.Select(patient => new DirectionInImmunologyLaboratoryReportViewModel
                {
                    CurrentOrganizationName = _localSettings.OrganizationName,
                    CurrentDepartmentName = _localSettings.DepartmentName,
                    DoctorNameWithInitials = _localSettings.MedExamDoctorName,
                    PatientFullName = patient.PersonName.FullName,
                    PatientAge = patient.BirthDate.HasValue
                                 ? patient.BirthDate.Value.GetYearsBefore(today).ToString()
                                 : "",
                    PatientOrganizationName = patient.OrganizationName
                });

                return datas;
            }
        }
    }
}