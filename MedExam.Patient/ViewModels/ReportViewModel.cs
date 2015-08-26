using Microsoft.Practices.Prism.ViewModel;

namespace MedExam.Patient.ViewModels
{
    public class ReportViewModel : NotificationObject
    {
        private bool _isSelected;

        public ReportViewModel(string name, string title = null)
        {
            Name = name;
            Title = title ?? name;
        }

        public string Name { get; private set; }
        public string Title { get; private set; }

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