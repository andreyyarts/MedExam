using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MedExam.Common;
using MedExam.Common.Interfaces;
using MedExam.Patient.services;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        private readonly ReportService _reportService;
        private readonly long[] _itemIds;
        private readonly IPrintService _printService;
        private readonly SystemService _systemService;

        public ReportListViewModel(ReportService reportService, long[] itemIds)
            : base(itemIds)
        {
            _reportService = reportService;
            _itemIds = itemIds;

            Reports = new List<ReportViewModel>(reportService.GetReports().Select(r => new ReportViewModel(r.Key, r.Value)));

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
            var reports = Reports.Where(r => r.IsSelected).Select(r => r.Name);
            _reportService.PrintReports(reports, _itemIds);
        }
    }
}