using MedExam.Common.interfaces;
using Microsoft.Practices.Prism.ViewModel;

namespace MedExam.Patient.ViewModels
{
    public class ReportViewModel : NotificationObject
    {
        private string _title;
        private bool _isSelected;

        public ReportViewModel(IReportFlow report, string title = null)
        {
            _title = title ?? report.Title;
            Report = report;
        }

        public ReportViewModel()
        {
        }

        public IReportFlow Report { get; private set; }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value.Equals(_isSelected)) return;
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
    }
}