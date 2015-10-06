using System;
using MedExam.Patient.dto;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace MedExam.Patient.ViewModels
{
    public class PatientViewModel : NotificationObject
    {
        private bool _isSelected;

        public PatientViewModel()
        {
            Select = new DelegateCommand(OnSelect);
        }

        public long Id { get; set; }
        public PersonName PersonName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public PolicyDto PolicyDto { get; set; }
        public string Gender { get; set; }
        public DelegateCommand Select { get; private set; }
        public string Organization { get; set; }

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

        private void OnSelect()
        {
            IsSelected = !IsSelected;
        }
    }
}