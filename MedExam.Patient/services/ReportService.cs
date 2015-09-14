using System.Collections.Generic;
using System.Linq;
using MedExam.Common;
using MedExam.Common.Extensions;
using MedExam.Common.Interfaces;
using MedExam.Patient.Reports;
using MedExam.Patient.Reports.ViewModels;
using Microsoft.Practices.ObjectBuilder2;

namespace MedExam.Patient.services
{
    public class ReportService
    {
        private readonly IPrintService _printService;
        private readonly PatientReportService _patientReportService;
        private readonly SystemService _systemService;
        private readonly LocalSettings _localSettings;
        private List<IReportFlow> _reports;

        public ReportService(IPrintService printService, PatientReportService patientReportService, SystemService systemService, LocalSettings localSettings)
        {
            _printService = printService;
            _patientReportService = patientReportService;
            _systemService = systemService;
            _localSettings = localSettings;
        }

        private IEnumerable<IReportFlow> Reports
        {
            get
            {
                return _reports
                       ?? (_reports = new IReportFlow[]
                       {
                           new BloodTestRpgaForTyphoidReport(this),
                           new SpecificTumorMarkerPsaReport(this),
                           new SpecificTumorMarkerCa125Report(this),
                           new ClinicalTrialEnterobiosisReport(this),
                           new ClinicalTrialReport(this),
                           new DirectionInBacteriologicalLaboratoryReport(this),
                       }
                           .OrderBy(r => r.Title)
                           .ToList());
            }
        }

        public Dictionary<string, string> GetReports()
        {
            return Reports.ToDictionary(r => r.GetType().Name, r => r.Title);
        }

        public void PrintReports(IEnumerable<string> reports, long[] itemIds, bool isPreview)
        {
            var selectedReports = Reports.Where(r => reports.Contains(r.GetType().Name)).ToArray();
            selectedReports.ForEach(r => r.SetItems(itemIds));
            _printService.PrintDocuments(selectedReports, isPreview);
        }

        public IEnumerable<DirectionReportViewModel> GetDirectionReportData(long[] patientIds)
        {
            var today = _systemService.Today();
            var patients = _patientReportService.LoadPatientsByIds(patientIds);

            var datas = patients.Select(patient => new DirectionReportViewModel
            {
                CurrentOrganizationName = _localSettings.OrganizationName,
                CurrentDepartmentName = _localSettings.DepartmentName,
                DoctorNameWithInitials = _localSettings.MedExamDoctorName,
                PatientFullName = patient.PersonName.FullName,
                PatientAge = patient.BirthDate.HasValue
                             ? patient.BirthDate.Value.GetYearsBefore(today).ToString()
                             : "",
                PatientOrganizationName = patient.OrganizationName,
                CurrentDate = today,
                HomeAddress = patient.Address
            });

            return datas;
        }
    }
}