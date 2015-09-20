using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MedExam.Common;
using MedExam.Patient.services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        private readonly ReportService _reportService;
        private readonly long[] _itemIds;
        
        public ReportListViewModel(ReportService reportService, long[] itemIds)
            : base(itemIds)
        {
            _reportService = reportService;
            _itemIds = itemIds;

            Reports = new List<ReportViewModel>(reportService.GetReports().Select(r => new ReportViewModel(r.Key, r.Value)));
            Reports.ForEach(r => r.PropertyChanged += ReportIsSelectedPropertyChanged);

            PrintText = new ObservableObject<string> { Value = "Печать (0)" };
            IsPreview = new ObservableObject<bool>();
            IsAllSelected = new ObservableObject<bool>();
            PrintReports = new DelegateCommand(OnPrintReports, CanPrintReports);
            IsAllSelected.PropertyChanged += AllSelectedChanged;
        }

        public ReportListViewModel() : base(new long[0]) { }

        public DelegateCommand PrintReports { get; set; }
        public List<ReportViewModel> Reports { get; set; }
        public ObservableObject<string> PrintText { get; set; }
        public ObservableObject<bool> IsAllSelected { get; set; }
        public ObservableObject<bool> IsPreview { get; set; }

        private void AllSelectedChanged(object sender, PropertyChangedEventArgs e)
        {
            Reports.ForEach(r => r.IsSelected = IsAllSelected.Value);
        }

        private void ReportIsSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected")
                return;

            PrintReports.RaiseCanExecuteChanged();

            var countSelectedReports = Reports.Count(r => r.IsSelected);
            PrintText.Value = string.Format("Печать ({0})", countSelectedReports);
        }

        private bool CanPrintReports()
        {
            return Reports.Any(r => r.IsSelected);
        }

        private void OnPrintReports()
        {
            var reports = Reports.Where(r => r.IsSelected).Select(r => r.Name);
            _reportService.PrintReports(reports, _itemIds, IsPreview.Value);
        }
    }
}