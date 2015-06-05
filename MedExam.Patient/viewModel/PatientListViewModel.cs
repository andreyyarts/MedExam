using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MedExam.Patient.dto;
using MedExam.Patient.services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.viewModel
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

        private void OnPrintReports()
        {
            ShowDebugMessage();
        }

        private void ShowDebugMessage()
        {
            var patients = Patients.Where(p => p.IsSelected);
            var patientsData =
                string.Join(", ", patients.Select(p => p.PersonName)
                    .Select(n => string.Concat(n.LastName, " ", n.FirstName)));

            MessageBox.Show(patientsData);
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
                BirthDate = patient.BirthDate,
                PersonName = PersonNameMap(patient.PersonName),
                Policy = patient.Policy,
                Gender = patient.Gender
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
