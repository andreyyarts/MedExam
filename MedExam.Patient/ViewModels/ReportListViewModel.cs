using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        private readonly IPrintService _printService;

        public ReportListViewModel() : base(new long[0]) { }

        public ReportListViewModel(IPrintService printService, long[] items)
            : base(items)
        {
            _printService = printService;

            Reports = new List<ReportViewModel>(new[]
            {
                new ReportViewModel(new DirectionInImmunologyLaboratoryReport(items))
            }).ToArray();

            PrintReports = new DelegateCommand(OnPrintReports, CanPrintReports);

            Reports.ForEach(r => r.PropertyChanged += ReportIsSelectedPropertyChanged);
        }

        public DelegateCommand PrintReports { get; set; }
        public ReportViewModel[] Reports { get; set; }

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
            var reports = Reports.Where(r => r.IsSelected).Select(r => r.Report).ToArray();
            _printService.PrintDocuments(reports);
        }
    }
}