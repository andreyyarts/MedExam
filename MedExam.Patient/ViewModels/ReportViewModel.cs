using Microsoft.Practices.Prism.ViewModel;

namespace MedExam.Patient.ViewModels
{
    public class ReportViewModel : NotificationObject
    {
        private string _title;
        private bool _isSelected;

        public ReportViewModel(string name, string title)
        {
            _title = title;
            Name = name;
        }

        public ReportViewModel(string name):this(name, name)
        {
        }

        public ReportViewModel()
        {
        }

        public string Name { get; private set; }

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