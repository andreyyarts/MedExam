using System.Collections.Generic;
using System.Linq;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports;
using Microsoft.Practices.ObjectBuilder2;

namespace MedExam.Patient.services
{
    public class ReportService
    {
        private readonly IPrintService _printService;
        private readonly PatientReportService _patientReportService;
        private readonly SystemService _systemService;
        private readonly LocalSettings _localSettings;
        private IReportFlow[] _reports;

        public ReportService(IPrintService printService, PatientReportService patientReportService, SystemService systemService, LocalSettings localSettings)
        {
            _printService = printService;
            _patientReportService = patientReportService;
            _systemService = systemService;
            _localSettings = localSettings;
        }

        private IReportFlow[] Reports
        {
            get
            {
                return _reports ?? (_reports = new IReportFlow[]
                {
                    new BloodTestRpgaForTyphoidReport(_localSettings, _systemService, _patientReportService)
                });
            }
        }

        public Dictionary<string, string> GetReports()
        {
            return Reports.ToDictionary(r => r.GetType().Name, r => r.Title);
        }

        public void PrintReports(IEnumerable<string> reports, long[] itemIds)
        {
            var selectedReports = Reports.Where(r => reports.Contains(r.GetType().Name)).ToArray();
            selectedReports.ForEach(r => r.SetItems(itemIds));
            _printService.PrintDocuments(selectedReports);
        }
    }
}