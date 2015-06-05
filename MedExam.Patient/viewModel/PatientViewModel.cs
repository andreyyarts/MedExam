using System;
using MedExam.Patient.dto;
using Microsoft.Practices.Prism.ViewModel;

namespace MedExam.Patient.viewModel
{
    public class PatientViewModel : NotificationObject
    {
        private bool _isSelected;
        public long Id { get; set; }
        public PersonName PersonName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public Policy Policy { get; set; }
        public Gender Gender { get; set; }

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