using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports;
using MedExam.Patient.services;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        private readonly IPrintService _printService;
        private readonly SystemService _systemService;

        public ReportListViewModel(IPrintService printService, PatientReportService patientReportService, SystemService systemService, long[] items)
            : base(items)
        {
            _printService = printService;
            _systemService = systemService;

            Reports = new List<ReportViewModel>(new[]
            {
                new ReportViewModel(new BloodTestRpgaForTyphoidReport(patientReportService, items))
            });

            Reports.ForEach(r => r.PropertyChanged += ReportIsSelectedPropertyChanged);
            PrintReports = new DelegateCommand(OnPrintReports, CanPrintReports);
        }

        public ReportListViewModel() : base(new long[0]) { }

        public DelegateCommand PrintReports { get; set; }
        public List<ReportViewModel> Reports { get; set; }

        private void ReportIsSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                PrintReports.RaiseCanExecuteChanged();
            }
        }

        private bool CanPrintReports()
        {
            return Reports.Any(r => r.IsSelected);
        }

        private void OnPrintReports()
        {
            var reports = Reports.Where(r => r.IsSelected).Select(r => r.Report);
            _printService.PrintDocuments(reports.ToArray());
        }
    }
}