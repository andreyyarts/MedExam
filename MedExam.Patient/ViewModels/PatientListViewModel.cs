using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MedExam.Common;
using MedExam.Patient.dto;
using MedExam.Patient.services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Patient.ViewModels
{
    public class PatientListViewModel
    {
        private readonly PatientService _patientService;
        private ICommand _refresh;
        private ICommand _printReports;

        public PatientListViewModel(OrganizationService organizationService, PatientService patientService)
        {
            _patientService = patientService;

            var organizations = organizationService.GetOrganizations();
            Organizations = new ListCollectionView(organizations);
            Patients = new ObservableCollection<PatientViewModel>();

            Organizations.CurrentChanged += (sender, args) =>
            {
                LoadPatients((OrganizationDto)Organizations.CurrentItem);
            };

            NotificationRequest = new InteractionRequest<Notification>();
        }

        public ObservableCollection<PatientViewModel> Patients { get; private set; }
        public ICollectionView Organizations { get; private set; }

        public ICommand Refresh
        {
            get { return _refresh ?? (_refresh = new DelegateCommand<OrganizationDto>(OnRefresh)); }
        }

        public ICommand PrintReports
        {
            get { return _printReports ?? (_printReports = new DelegateCommand(OnPrintReports)); }
        }

        public InteractionRequest<Notification> NotificationRequest { get; set; }

        private void OnPrintReports()
        {
            var patientIds = Patients.Where(p => p.IsSelected).Select(p => p.Id).ToArray();

            ShowDebugMessage(new ItemsNotification<long>(patientIds) { Title = "Печать" });
        }

        private void ShowDebugMessage(Notification notification)
        {
            NotificationRequest.Raise(notification);
        }

        private void OnRefresh(OrganizationDto organization)
        {
            LoadPatients(organization);
        }

        private void LoadPatients(OrganizationDto organization)
        {
            if (organization == null)
                throw new NullReferenceException("organization cannot be null");

            Patients.Clear();
            var patients = _patientService.GetPatientsByOrganizationId(organization.Id);
            Patients.AddRange(patients.Select(PatientDtoMap));
        }

        private static PatientViewModel PatientDtoMap(PatientDto patient)
        {
            return new PatientViewModel
            {
                Id = patient.Id,
                Address = patient.Address,
                BirthDate = patient.BirthDate.HasValue ? patient.BirthDate.Value.ToShortDateString() : "",
                PersonName = PersonNameMap(patient.PersonName),
                Policy = patient.Policy,
                Gender = patient.Gender == Gender.Female ? "Ж" : "М"
            };
        }

        private static PersonName PersonNameMap(PersonNameDto name)
        {
            var names = !string.IsNullOrEmpty(name.FirstNameAndMiddleName) ? name.FirstNameAndMiddleName.Split(' ', '.', ',') : new string[0];

            return new PersonName
            {
                LastName = name.LastName,
                FirstName = names.Length > 0 ? names[0] : "",
                MiddleName = names.Length > 1 ? names[1] : ""
            };
        }
    }
}
